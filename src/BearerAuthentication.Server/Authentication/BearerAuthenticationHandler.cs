using System.Threading.Tasks;
using Microsoft.AspNet.Authentication;
using Microsoft.AspNet.Http.Authentication;
using Microsoft.AspNet.Http.Features.Authentication;
using Microsoft.Extensions.Primitives;

namespace BearerAuthentication.Server.Authentication
{
    public class BearerAuthenticationHandler : AuthenticationHandler<BearerAuthenticationOptions>
    {
        private bool _shouldRenew;

        private AuthenticationTicket GetTicket()
        {
            if (!Context.Request.Headers.ContainsKey(Options.HeaderName))
                return null;
            var bearer = Context.Request.Headers[Options.HeaderName];
            if (string.IsNullOrEmpty(bearer))
                return null;

            var ticket = Options.TicketDataFormat.Unprotect(bearer);
            if (ticket == null)
                return null;

            var currentUtc = Options.SystemClock.UtcNow;
            var expiresUtc = ticket.Properties.ExpiresUtc;
            if (expiresUtc.HasValue && expiresUtc.Value < currentUtc)
                return null;

            return ticket;
        }

        private void ApplyBearer(AuthenticationTicket ticket)
        {
            if (ticket != null)
            {
                var protectedData = Options.TicketDataFormat.Protect(ticket);
                Response.Headers["Access-Control-Expose-Headers"] = Options.HeaderName;
                Response.Headers[Options.HeaderName] = protectedData;
            }
            else
            {
                Response.Headers["Access-Control-Expose-Headers"] = Options.HeaderName;
                Response.Headers[Options.HeaderName] = StringValues.Empty;
            }
        }

        protected override async Task HandleSignInAsync(SignInContext signIn)
        {
            var signingInContext = new BearerSigningInContext(Context, Options, signIn.Principal, new AuthenticationProperties(signIn.Properties));
            await Options.Events.SigningIn(signingInContext);

            var ticket = new AuthenticationTicket(signingInContext.Principal, signingInContext.Properties, Options.AuthenticationScheme);
            ApplyBearer(ticket);

            var signedInContext = new BearerSignedInContext(Context, Options, signingInContext.Principal, signingInContext.Properties);
            await Options.Events.SignedIn(signedInContext);
        }

        protected override async Task HandleSignOutAsync(SignOutContext context)
        {
            var signingOutContext = new BearerSigningOutContext(Context, Options);
            await Options.Events.SigningOut(signingOutContext);
            ApplyBearer(null);
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var ticket = GetTicket();
            if (ticket == null)
                return AuthenticateResult.Failed("No ticket.");

            var context = new BearerValidatePrincipalContext(Context, Options, ticket.Principal, ticket.Properties);
            await Options.Events.ValidatePrincipal(context);
            if (context.Principal == null)
                return AuthenticateResult.Failed("No principal.");

            if (context.ShouldRenew)
                _shouldRenew = true;

            return AuthenticateResult.Success(new AuthenticationTicket(context.Principal, context.Properties, Options.AuthenticationScheme));
        }

        protected override async Task<bool> HandleUnauthorizedAsync(ChallengeContext context)
        {
            Response.StatusCode = 401;

            var unauthorizedContext = new BearerUnauthorizedContext(Context, Options);
            await Options.Events.Unauthorized(unauthorizedContext);
            return true;
        }

        protected override async Task<bool> HandleForbiddenAsync(ChallengeContext context)
        {
            Response.StatusCode = 403;

            var forbiddenContext = new BearerForbiddenContext(Context, Options);
            await Options.Events.Forbidden(forbiddenContext);
            return true;
        }

        protected override async Task FinishResponseAsync()
        {
            if (!_shouldRenew || SignInAccepted || SignOutAccepted)
                return;

            var result = await HandleAuthenticateOnceAsync();
            var ticket = result?.Ticket;
            if (ticket == null)
                return;

            ApplyBearer(ticket);
        }
    }
}
