using Microsoft.AspNet.Http;

namespace BearerAuthentication.Server.Authentication
{
    public class BearerSigningOutContext : BearerBaseContext
    {
        public BearerSigningOutContext(HttpContext context, BearerAuthenticationOptions options) : base(context, options)
        {
        }
    }
}
