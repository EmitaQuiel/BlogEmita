using BlogEmi.Models;
using BlogEmi.Services.Contract;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BlogEmi.Controllers
{
    
        public class ProfileController : Controller
        {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        public async Task<IActionResult> Profile()
        {
            var userName = User.Identity?.Name ?? "DefaultUser";
            var profile = await _profileService.GetProfileByUserName(userName);
            return View(profile);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserProfile profile, IFormFile? image)
        {
            if (image != null && image.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await image.CopyToAsync(memoryStream);
                    profile.Image = memoryStream.ToArray();
                }
            }

            await _profileService.UpdateProfile(profile);
            TempData["Mensaje"] = "Profile updated successfully.";
            return RedirectToAction("Profile");
        }

    }
}

