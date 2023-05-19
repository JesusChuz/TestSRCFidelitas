using Microsoft.AspNetCore.Mvc;

namespace sistema_reconocimiento.Controllers
{
    public class UCController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Mis_Reconocimientos()
        {
            return View();
        }
        public IActionResult Reconocer()
        {
            return View();
        }
        public IActionResult Perfil()
        {
            return View();
        }
    }
}
