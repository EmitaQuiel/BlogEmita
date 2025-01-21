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
        public HomeController(IUserService UserServicio)
        {
            _UserServicio = UserServicio;
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
                new Claim(ClaimTypes.Name, userFound.FullName)
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
