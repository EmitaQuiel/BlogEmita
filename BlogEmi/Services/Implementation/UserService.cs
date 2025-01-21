using BlogEmi.Data;
using BlogEmi.Models;
using BlogEmi.Services.Contract;
using Microsoft.EntityFrameworkCore;

namespace BlogEmi.Services.Implementation
{
    public class UserService : IUserService
    {

        private readonly AppDbContext _dbContext;
        public UserService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<User> GetUser(string email, string password)
        {
            User userFound = await _dbContext.Users.Where(u => u.Email == email && u.Password == password)
                .FirstOrDefaultAsync();

            return userFound;
        }

        public async Task<User> SaveUser(User modelo)
        {
            _dbContext.Users.Add(modelo);
            await _dbContext.SaveChangesAsync();
            return modelo;
        }
    }
}
