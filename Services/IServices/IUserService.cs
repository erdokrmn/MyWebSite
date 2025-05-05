using MyWebSite.Models;

namespace MyWebSite.Services.IServices
{
    public interface IUserService
    {
        LoginResult ValidateUser(string username, string password);

    }
}
