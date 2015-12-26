using System;
using System.Collections.Generic;
using System.Linq;
using BearerAuthentication.Server.Models;

namespace BearerAuthentication.Server.Services
{
    public class UserService : IUserService
    {
        private readonly IEnumerable<User> _users = new List<User>
        {
            new User
            {
                UserId = 1,
                Login = "Bob",
                Password = "Bob",
                FullName = "Bob Smith",
                IsActive = true
            },

            new User
            {
                UserId = 2,
                Login = "Alice",
                Password = "Alice",
                FullName = "Alice Johnson",
                IsActive = true
            }
        };

        public User GetUser(string login)
        {
            return _users.FirstOrDefault(x => x.Login.Equals(login, StringComparison.CurrentCultureIgnoreCase));
        }

        public User GetUser(int userId)
        {
            return _users.FirstOrDefault(x => x.UserId == userId);
        }
    }
}
