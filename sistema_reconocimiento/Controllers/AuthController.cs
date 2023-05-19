using Microsoft.AspNetCore.Mvc;

namespace sistema_reconocimiento.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Recuperar_password()
        {
            return View();
        }
        public IActionResult Cambio_password()
        {
            return View();
        }
    }
}
