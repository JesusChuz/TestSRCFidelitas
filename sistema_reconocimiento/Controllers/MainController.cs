using Microsoft.AspNetCore.Mvc;
using sistema_reconocimiento.Models;
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

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Ingenieros()
        {
            return View();
        }
        public IActionResult Agregar_ingeniero()
        {
            return View();
        }
        public IActionResult Editar_ingeniero()
        {
            return View();
        }
        public IActionResult Reconocimientos()
        {
            return View();
        }
        public IActionResult Recompensas()
        {
            return View();
        }
        public IActionResult Agregar_recompensa()
        {
            return View();
        }
        public IActionResult Editar_recompensa()
        {
            return View();
        }
        public IActionResult Frases()
        {
            return View();
        }
        public IActionResult Agregar_frase()
        {
            return View();
        }
        public IActionResult Editar_frase()
        {
            return View();
        }
        public IActionResult Mis_Reconocimientos()
        {
            return View();
        }
        public IActionResult Perfil()
        {
            return View();
        }
        public IActionResult Reconocer()
        {
            return View();
        }
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