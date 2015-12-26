using Microsoft.AspNet.Authorization;

namespace BearerAuthentication.Server.Authentication
{
    public class UserRequirement : AuthorizationHandler<UserRequirement>, IAuthorizationRequirement
    {
        protected override void Handle(AuthorizationContext context, UserRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == "UserId"))
            {
                context.Fail();
                return;
            }
            context.Succeed(requirement);
        }
    }
}
