using System;
using Microsoft.AspNet.Authentication;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.DataProtection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.WebEncoders;

namespace BearerAuthentication.Server.Authentication
{
    public class BearerAuthenticationMiddleware : AuthenticationMiddleware<BearerAuthenticationOptions>
    {
        public BearerAuthenticationMiddleware(RequestDelegate next, ILoggerFactory loggerFactory, IUrlEncoder urlEncoder, IDataProtectionProvider dataProtectionProvider, BearerAuthenticationOptions options) : base(next, options, loggerFactory, urlEncoder)
        {
            if (next == null)
                throw new ArgumentNullException(nameof(next));
            if (loggerFactory == null)
                throw new ArgumentNullException(nameof(loggerFactory));
            if (urlEncoder == null)
                throw new ArgumentNullException(nameof(urlEncoder));
            if (options == null)
                throw new ArgumentNullException(nameof(options));
            if (string.IsNullOrEmpty(Options.HeaderName))
                Options.HeaderName = BearerAuthenticationDefaults.HeaderName;
            if (Options.Events == null)
                Options.Events = new BearerAuthenticationEvents();
            if (Options.TicketDataFormat == null)
                Options.TicketDataFormat = new TicketDataFormat((Options.DataProtectionProvider ?? dataProtectionProvider).CreateProtector(typeof(BearerAuthenticationMiddleware).FullName, Options.AuthenticationScheme));
        }

        protected override AuthenticationHandler<BearerAuthenticationOptions> CreateHandler()
        {
            return new BearerAuthenticationHandler();
        }
    }
}
