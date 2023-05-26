using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sistema_reconocimiento.Models;
using System.Data;
using System.Diagnostics;

namespace sistema_reconocimiento.Controllers
{
    public class MainController : Controller
    {
        private readonly ILogger<MainController> _logger;

        public MainController(ILogger<MainController> logger)
        {
            _logger = logger;
        }
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "admin")]
        public IActionResult Ingenieros()
        {
            return View();
        }
        [Authorize(Roles = "admin")]
        public IActionResult Agregar_ingeniero()
        {
            return View();
        }
        [Authorize(Roles = "admin")]
        public IActionResult Editar_ingeniero()
        {
            return View();
        }
        [Authorize(Roles = "admin")]
        public IActionResult Reconocimientos()
        {
            return View();
        }
        [Authorize(Roles = "admin")]
        public IActionResult Recompensas()
        {
            return View();
        }
        [Authorize(Roles = "admin")]
        public IActionResult Agregar_recompensa()
        {
            return View();
        }
        [Authorize(Roles = "admin")]
        public IActionResult Editar_recompensa()
        {
            return View();
        }
        [Authorize(Roles = "admin")]
        public IActionResult Frases()
        {
            return View();
        }
        [Authorize(Roles = "admin")]
        public IActionResult Agregar_frase()
        {
            return View();
        }
        [Authorize(Roles = "admin")]
        public IActionResult Editar_frase()
        {
            return View();
        }
        [Authorize]
        public IActionResult Mis_Reconocimientos()
        {
            return View();
        }
        [Authorize]
        public IActionResult Perfil()
        {
            return View();
        }
        [Authorize]
        public IActionResult Reconocer()
        {
            return View();
        }
        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}