using System;
using System.Threading.Tasks;

namespace BearerAuthentication.Server.Authentication
{
    public class BearerAuthenticationEvents : IBearerAuthenticationEvents
    {
        public Func<BearerValidatePrincipalContext, Task> OnValidatePrincipal { get; set; } = context => Task.FromResult<object>(null);

        public Func<BearerSigningInContext, Task> OnSigningIn { get; set; } = context => Task.FromResult<object>(null);

        public Func<BearerSignedInContext, Task> OnSignedIn { get; set; } = context => Task.FromResult<object>(null);

        public Func<BearerSigningOutContext, Task> OnSigningOut { get; set; } = context => Task.FromResult<object>(null);

        public Func<BearerUnauthorizedContext, Task> OnUnauthorized { get; set; } = context => Task.FromResult<object>(null);

        public Func<BearerForbiddenContext, Task> OnForbidden { get; set; } = context => Task.FromResult<object>(null);

        public Task ValidatePrincipal(BearerValidatePrincipalContext context)
        {
            return OnValidatePrincipal(context);
        }

        public Task SigningIn(BearerSigningInContext context)
        {
            return OnSigningIn(context);
        }

        public Task SignedIn(BearerSignedInContext context)
        {
            return OnSignedIn(context);
        }

        public Task SigningOut(BearerSigningOutContext context)
        {
            return OnSigningOut(context);
        }

        public Task Unauthorized(BearerUnauthorizedContext context)
        {
            return OnUnauthorized(context);
        }

        public Task Forbidden(BearerForbiddenContext context)
        {
            return OnForbidden(context);
        }
    }
}
