using Microsoft.AspNet.Http;

namespace BearerAuthentication.Server.Authentication
{
    public class BearerUnauthorizedContext : BearerBaseContext
    {
        public BearerUnauthorizedContext(HttpContext context, BearerAuthenticationOptions options) : base(context, options)
        {
        }
    }
}
