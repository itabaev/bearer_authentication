using System.Security.Claims;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Http.Authentication;

namespace BearerAuthentication.Server.Authentication
{
    public class BearerSignedInContext : BearerBaseContext
    {
        public ClaimsPrincipal Principal { get; }

        public AuthenticationProperties Properties { get; }

        public BearerSignedInContext(HttpContext context, BearerAuthenticationOptions options, ClaimsPrincipal principal, AuthenticationProperties properties) : base(context, options)
        {
            Principal = principal;
            Properties = properties;
        }
    }
}
