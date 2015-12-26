using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using BearerAuthentication.Server.Authentication;
using BearerAuthentication.Server.Services;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;

namespace BearerAuthentication.Server.Controllers
{
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private IUserService UserService { get; }

        public AuthController(IUserService userService)
        {
            UserService = userService;
        }

        [HttpPost]
        public async void LogIn([FromForm]string login, [FromForm]string password)
        {
            if (login == null || password == null)
                throw new Exception("Incorrect login or password");

            var user = UserService.GetUser(login);
            if (user == null || !user.Password.Equals(password))
                throw new Exception("Incorrect login or password");

            var principal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
            {
                new Claim("UserId", user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.FullName),
            }, BearerAuthenticationDefaults.AuthenticationScheme));

            await HttpContext.Authentication.SignInAsync(BearerAuthenticationDefaults.AuthenticationScheme, principal);
        }

        [HttpDelete]
        public async void LogOut()
        {
            await HttpContext.Authentication.SignOutAsync(BearerAuthenticationDefaults.AuthenticationScheme);
        }

        [Route("test")]
        [Authorize]
        public string Test()
        {
            return User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
        }
    }
}
