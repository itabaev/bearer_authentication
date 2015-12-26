using System.Security.Claims;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Http.Authentication;

namespace BearerAuthentication.Server.Authentication
{
    public class BearerSigningInContext : BearerBaseContext
    {
        public ClaimsPrincipal Principal { get; set; }

        public AuthenticationProperties Properties { get; set; }

        public BearerSigningInContext(HttpContext context, BearerAuthenticationOptions options, ClaimsPrincipal principal, AuthenticationProperties properties) : base(context, options)
        {
            Principal = principal;
            Properties = properties;
        }
    }
}
