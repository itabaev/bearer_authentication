using Microsoft.AspNet.Authentication;
using Microsoft.AspNet.DataProtection;
using Microsoft.Extensions.OptionsModel;

namespace BearerAuthentication.Server.Authentication
{
    public class BearerAuthenticationOptions : AuthenticationOptions, IOptions<BearerAuthenticationOptions>
    {
        public BearerAuthenticationOptions()
        {
            AuthenticationScheme = BearerAuthenticationDefaults.AuthenticationScheme;
            HeaderName = BearerAuthenticationDefaults.HeaderName;
            SystemClock = new SystemClock();
            Events = new BearerAuthenticationEvents();
        }

        public string HeaderName { get; set; }

        public ISecureDataFormat<AuthenticationTicket> TicketDataFormat { get; set; }

        public IDataProtectionProvider DataProtectionProvider { get; set; }

        public ISystemClock SystemClock { get; set; }

        public IBearerAuthenticationEvents Events { get; set; }

        public BearerAuthenticationOptions Value => this;
    }
}
