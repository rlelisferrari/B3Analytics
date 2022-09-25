using Aspnet_AuthCookies1.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Aspnet_AuthCookies1.Controllers
{
    public class LoginController : Controller
    {
        private readonly IConfiguration configuration;

        public LoginController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpGet]
        public ActionResult UsuarioLogin()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> UsuarioLogin([Bind] Usuario _usuario)
        {
            var listUsers = await _usuario.GetUsuarios(configuration["ConnectionStrings:Default"]);

            if(!listUsers.Item1)
            {
                ViewBag.Message = "Nao foi possível conectar com o banco de dados loteria_lotep";
                return View(_usuario);
            }

            if (listUsers.Item2.Any(user => user.Login.ToLower() == _usuario.Login.ToLower() && user.Password.ToLower() == _usuario.Password))
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