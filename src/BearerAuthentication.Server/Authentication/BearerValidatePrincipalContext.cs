using System.Security.Claims;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Http.Authentication;

namespace BearerAuthentication.Server.Authentication
{
    public class BearerValidatePrincipalContext : BearerBaseContext
    {
        public ClaimsPrincipal Principal { get; private set; }

        public AuthenticationProperties Properties { get; }

        public bool ShouldRenew { get; set; }

        public BearerValidatePrincipalContext(HttpContext context, BearerAuthenticationOptions options, ClaimsPrincipal principal, AuthenticationProperties properties) : base(context, options)
        {
            Principal = principal;
            Properties = properties;
        }
        
        public void ReplacePrincipal(ClaimsPrincipal principal)
        {
            Principal = principal;
        }
        
        public void RejectPrincipal()
        {
            Principal = null;
        }
    }
}
