using BlogEmi.Models;

namespace BlogEmi.Services.Contract
{
    public interface IUserService
    {
        Task<User> GetUser(string email, string password);
        Task<User> SaveUser(User modelo);
    }
}
