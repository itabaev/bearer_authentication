using Microsoft.AspNet.Http;

namespace BearerAuthentication.Server.Authentication
{
    public class BearerForbiddenContext : BearerBaseContext
    {
        public BearerForbiddenContext(HttpContext context, BearerAuthenticationOptions options) : base(context, options)
        {
        }
    }
}
