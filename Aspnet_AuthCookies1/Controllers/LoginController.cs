using Aspnet_AuthCookies1.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Aspnet_AuthCookies1.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult UsuarioLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UsuarioLogin([Bind] Usuario _usuario)
        {
            var usuario = new Usuario();

            if (usuario.GetUsuarios().Any(u => u.Login == _usuario.Login && u.Password == _usuario.Password))
            {
                var userClaims = new List<Claim>()
                {
                    //define o cookie
                    new Claim(ClaimTypes.Name, _usuario.Login),
                    new Claim(ClaimTypes.Email, "lelisolutions@gmail.com"),
                };

                var minhaIdentity = new ClaimsIdentity(userClaims, "Usuario");

                var userPrincipal = new ClaimsPrincipal(new[] { minhaIdentity });
                //cria o cookie
                HttpContext.SignInAsync(userPrincipal);

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Message = "Credenciais inválidas...";

            return View(_usuario);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}