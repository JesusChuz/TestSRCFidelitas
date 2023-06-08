using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using sistema_reconocimiento.Models;
using System.Data;
using System.Diagnostics;
using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using sistema_reconocimiento.Interface;
using System.Configuration;
using Microsoft.AspNetCore.Http;
using sistema_reconocimiento.Data;
using Microsoft.EntityFrameworkCore;

namespace sistema_reconocimiento.Controllers
{
    public class MainController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<MainController> _logger;
        private readonly IConfiguration _configuration;
        private readonly string _varConnStr;
        public MainController(ILogger<MainController> logger, IConfiguration configuration, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
            _varConnStr = _configuration.GetValue<string>("ConnectionStrings:DBConnectionString");
        }
        public bool validateAccountEnabled(bool result)
        {
            // Crear la conexión a la base de datos
            /*if(model.Email == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            else
            {*/
                // Obtener el objeto de sesión
                ISession session = HttpContext.Session;
                // Obtener el valor almacenado en la sesión
                string emailSession = session.GetString("EmailSession");

                using (SqlConnection connection = new SqlConnection(_varConnStr))
                {
                    try
                    {
                        connection.Open();
                        // Crear el comando y asignar el stored procedure
                        using (SqlCommand command = new SqlCommand("GetLockoutEnabledAndIsNew", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            // Agregar parámetro de entrada
                            command.Parameters.AddWithValue("@Email", emailSession);
                            // Ejecutar el stored procedure y leer los resultados
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    bool lockoutEnabled = (bool)reader["LockoutEnabled"];
                                    bool isNew = (bool)reader["IsNew"];
                                    if (lockoutEnabled == true || isNew == true) //-> redirija a la vista de cambio de password para que al cambiarla, se desbloquee (pase lockoutEnabled a false y isNew a false)
                                    {
                                        result = false;
                                        return result;
                                    //return RedirectToAction("Cambio_password", "Auth");
                                    }   
                                    else
                                    {
                                         result = true;
                                         return result;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("No se encontraron resultados.");
                                    result = false;
                                    return result;
                                //return RedirectToAction("Login", "Auth");
                            }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                        connection.Close();
                        result = false;
                        return result;
                        //return RedirectToAction("Login", "Auth");
                    }
                return result;
                //}
            }
        }
        [Authorize]
        public IActionResult Index(bool result)
        {
            if (result == false) {
                result = validateAccountEnabled(result);
                if (result == true)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Login", "Auth");
                }
            }
            return RedirectToAction("Login", "Auth");
        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> IngenierosAsync(bool result)
        {
            if (result == false)
            {
                result = validateAccountEnabled(result);
                if (result == true)
                {   
                    var applicationDbContext = _context.Engineers.Include(e => e.Manager).Include(e => e.Positions);
                    return View(await applicationDbContext.ToListAsync());
                }
                else
                {
                    return RedirectToAction("Login", "Auth");
                }
            }
            return RedirectToAction("Login", "Auth");
        }
        [Authorize(Roles = "admin")]
        public IActionResult Agregar_ingeniero(bool result)
        {
            if (result == false)
            {
                result = validateAccountEnabled(result);
                if (result == true)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Login", "Auth");
                }
            }
            return RedirectToAction("Login", "Auth");
        }
        [Authorize(Roles = "admin")]
        public IActionResult Editar_ingeniero(bool result)
        {
            if (result == false)
            {
                result = validateAccountEnabled(result);
                if (result == true)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Login", "Auth");
                }
            }
            return RedirectToAction("Login", "Auth");
        }
        [Authorize(Roles = "admin")]
        public IActionResult Reconocimientos(bool result)
        {
            if (result == false)
            {
                result = validateAccountEnabled(result);
                if (result == true)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Login", "Auth");
                }
            }
            return RedirectToAction("Login", "Auth");
        }
        [Authorize(Roles = "admin")]
        public IActionResult Recompensas(bool result)
        {
            if (result == false)
            {
                result = validateAccountEnabled(result);
                if (result == true)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Login", "Auth");
                }
            }
            return RedirectToAction("Login", "Auth");
        }
        [Authorize(Roles = "admin")]
        public IActionResult Agregar_recompensa(bool result)
        {
            if (result == false)
            {
                result = validateAccountEnabled(result);
                if (result == true)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Login", "Auth");
                }
            }
            return RedirectToAction("Login", "Auth");
        }
        [Authorize(Roles = "admin")]
        public IActionResult Editar_recompensa(bool result)
        {
            if (result == false)
            {
                result = validateAccountEnabled(result);
                if (result == true)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Login", "Auth");
                }
            }
            return RedirectToAction("Login", "Auth");
        }
        [Authorize(Roles = "admin")]
        public IActionResult Frases(bool result)
        {
            if (result == false)
            {
                result = validateAccountEnabled(result);
                if (result == true)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Login", "Auth");
                }
            }
            return RedirectToAction("Login", "Auth");
        }
        [Authorize(Roles = "admin")]
        public IActionResult Agregar_frase(bool result)
        {
            if (result == false)
            {
                result = validateAccountEnabled(result);
                if (result == true)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Login", "Auth");
                }
            }
            return RedirectToAction("Login", "Auth");
        }
        [Authorize(Roles = "admin")]
        public IActionResult Editar_frase(bool result)
        {
            if (result == false)
            {
                result = validateAccountEnabled(result);
                if (result == true)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Login", "Auth");
                }
            }
            return RedirectToAction("Login", "Auth");
        }
        [Authorize]
        public IActionResult Mis_Reconocimientos(bool result)
        {
            if (result == false)
            {
                result = validateAccountEnabled(result);
                if (result == true)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Login", "Auth");
                }
            }
            return RedirectToAction("Login", "Auth");
        }
        [Authorize]
        public IActionResult Perfil(bool result)
        {
            if (result == false)
            {
                result = validateAccountEnabled(result);
                if (result == true)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Login", "Auth");
                }
            }
            return RedirectToAction("Login", "Auth");
        }
        [Authorize]
        public IActionResult Reconocer(bool result)
        {
            if (result == false)
            {
                result = validateAccountEnabled(result);
                if (result == true)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Login", "Auth");
                }
            }
            return RedirectToAction("Login", "Auth");
        }
        [Authorize]
        public IActionResult Privacy(bool result)
        {
            if (result == false)
            {
                result = validateAccountEnabled(result);
                if (result == true)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Login", "Auth");
                }
            }
            return RedirectToAction("Login", "Auth");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}