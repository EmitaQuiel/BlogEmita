using BlogEmi.Data;
using BlogEmi.Models;
using BlogEmi.Services.Contract;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;


    namespace BlogEmi.Services.Implementation
    {

    public class ProfileService : IProfileService
    {
        private readonly AppDbContext _context; 

        public ProfileService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserProfile> GetProfileByUserName(string userName)
        {
            var userProfile = await _context.UsersProfiles
                .FirstOrDefaultAsync(up => up.UserName == userName);

            if (userProfile == null)
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.UserName == userName);

                if (user != null)
                {
                    userProfile = new UserProfile
                    {
                        UserName = user.UserName,
                        UserId = user.IdUser 
                    };

                    _context.UsersProfiles.Add(userProfile);
                    await _context.SaveChangesAsync();
                }
            }

            return userProfile;
        }

        public async Task UpdateProfile(UserProfile profile)
        {
            var existingProfile = await _context.UsersProfiles
                .FirstOrDefaultAsync(p => p.UserId == profile.UserId);

            if (existingProfile != null)
            {
                existingProfile.Description = profile.Description;
                existingProfile.Image = profile.Image;

                _context.UsersProfiles.Update(existingProfile);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("El perfil no existe.");
            }
        }
    }
}



