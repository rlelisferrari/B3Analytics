using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Aspnet_AuthCookies1.Models;

namespace Aspnet_AuthCookies1.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            this._logger = logger;
            this.configuration = configuration;
        }

        public IActionResult Index()
        {
            var usuario = "Anônimo";
            var autenticado = false;
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                usuario = HttpContext.User.Identity.Name;
                autenticado = true;
            }
            else
            {
                usuario = "Não Logado";
                autenticado = false;
            }

            ViewBag.usuario = usuario;
            ViewBag.autenticado = autenticado;
            this._logger.LogInformation($"{usuario} acessou a página home");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Teste()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}