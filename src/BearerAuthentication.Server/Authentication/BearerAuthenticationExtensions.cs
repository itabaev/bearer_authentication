using System;
using Microsoft.AspNet.Builder;

namespace BearerAuthentication.Server.Authentication
{
    public static class BearerAuthenticationExtensions
    {
        public static IApplicationBuilder UseBearerAuthentication(this IApplicationBuilder app)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));
            return UseBearerAuthentication(app, new BearerAuthenticationOptions());
        }

        public static IApplicationBuilder UseBearerAuthentication(this IApplicationBuilder app, Action<BearerAuthenticationOptions> configureOptions)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));
            var options = new BearerAuthenticationOptions();
            configureOptions?.Invoke(options);
            return UseBearerAuthentication(app, options);
        }

        public static IApplicationBuilder UseBearerAuthentication(this IApplicationBuilder app, BearerAuthenticationOptions options)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));
            if (options == null)
                throw new ArgumentNullException(nameof(options));
            return app.UseMiddleware<BearerAuthenticationMiddleware>(options);
        }
    }
}