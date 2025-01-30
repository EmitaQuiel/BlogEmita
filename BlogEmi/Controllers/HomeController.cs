using BlogEmi.Services.Contract;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Security.Claims;
using BlogEmi.Models;
using BlogEmi.Resources;
using BlogEmi.Services.Implementation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace BlogEmi.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _UserServicio;
        private readonly IPostService _postService;
        private readonly IProfileService _profileService;
        public HomeController(IUserService UserServicio, IPostService PostService, IProfileService ProfileService)
        {
            _UserServicio = UserServicio;
            _postService = PostService;
            _profileService = ProfileService;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User modelo)
        {
            modelo.Password = Utilities.EncryptKey(modelo.Password);
            User usuarioEncontrado = await _UserServicio.SaveUser(modelo);

            if (usuarioEncontrado.IdUser > 0)
                return RedirectToAction("Login", "Home");

            ViewData["Mensaje"] = "No se pudo crear el usuario";
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }


        public async Task<IActionResult> Blog()
        {
            var userName = User.Identity?.Name ?? "DefaultUser"; // Obtiene el nombre del usuario autenticado
            var profile = await _profileService.GetProfileByUserName(userName);
            var posts = await _postService.GetAllPosts();

            var viewModel = new BlogViewModel
            {
                UserProfile = profile,
                Posts = posts
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            User userFound = await _UserServicio.GetUser(email, Utilities.EncryptKey(password));
            if (userFound == null)
            {
                ViewData["Message"] = "Don't found coincidence";
                return View();
            }
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userFound.UserName)
            };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties properties = new AuthenticationProperties
            {
                AllowRefresh = true
            };
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                properties
            );

            return RedirectToAction("Blog");

        }


    }
}
