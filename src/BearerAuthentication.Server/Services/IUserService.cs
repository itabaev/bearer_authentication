using BearerAuthentication.Server.Models;

namespace BearerAuthentication.Server.Services
{
    public interface IUserService
    {
        User GetUser(string login);

        User GetUser(int userId);
    }
}
