namespace BearerAuthentication.Server.Models
{
    public class User
    {
        public int UserId { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string FullName { get; set; }

        public bool IsActive { get; set; }
    }
}
