using BlogEmi.Data;
using BlogEmi.Models;
using BlogEmi.Services.Contract;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BlogEmi.Services.Implementation
{
   
        public class ProfileService : IProfileService
        {
            private readonly List<UserProfile> _profiles = new(); // Simulación de almacenamiento temporal

            public Task<UserProfile> GetProfileByUserName(string userName)
            {
                // Busca el perfil por UserName o crea uno nuevo por defecto
                var profile = _profiles.FirstOrDefault(p => p.UserName == userName);
                if (profile == null)
                {
                    profile = new UserProfile { UserName = userName }; // Crea perfil por defecto
                    _profiles.Add(profile);
                }
                return Task.FromResult(profile);
            }

            public Task UpdateProfile(UserProfile profile)
            {
                // Actualiza el perfil
                var existingProfile = _profiles.FirstOrDefault(p => p.UserName == profile.UserName);
                if (existingProfile != null)
                {
                    existingProfile.Description = profile.Description;
                    existingProfile.Image = profile.Image;
                }
                return Task.CompletedTask;
            }
        }





    }


