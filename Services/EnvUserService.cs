using MyWebSite.Models;
using MyWebSite.Services.IServices;

namespace MyWebSite.Services
{
    public class EnvUserService : IUserService
    {
        public LoginResult ValidateUser(string username, string password)
        {
            var envUsername = Environment.GetEnvironmentVariable("ADMIN_USERNAME");
            var envPassword = Environment.GetEnvironmentVariable("ADMIN_PASSWORD");

            if (string.IsNullOrWhiteSpace(envUsername) || string.IsNullOrWhiteSpace(envPassword))
            {
                return new LoginResult { Success = false, ErrorMessage = "Sunucu admin bilgilerini içermiyor." };
            }

            if (username == envUsername && password == envPassword)
            {
                return new LoginResult
                {
                    Success = true,
                    User = new User
                    {
                        Id = 1,
                        UserName = envUsername,
                        Role = "Admin"
                    }
                };
            }

            return new LoginResult { Success = false, ErrorMessage = "Kullanıcı adı veya şifre yanlış." };
        }
    }
}
