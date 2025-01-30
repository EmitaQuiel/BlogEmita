using BlogEmi.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BlogEmi.Services.Contract
{
    public interface IProfileService
        {
        Task<UserProfile> GetProfileByUserName(string userName);
        Task UpdateProfile(UserProfile profile);
    }
    
}
