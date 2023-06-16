#nullable disable

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using sistema_reconocimiento.Models;
using sistema_reconocimiento.Interface;
using System.Configuration;
using Microsoft.AspNetCore.Http;
using System.Drawing;
<<<<<<< HEAD
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using sistema_reconocimiento.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;
=======
>>>>>>> 5214b57e3f10b832105456c72dacff5b1de60d2b
// se manda a llamar los metodos que se declararon en la interface con unos ultimos detalles
namespace sistema_reconocimiento.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _service;
        private readonly IConfiguration _configuration;
        private readonly string _varConnStr;
<<<<<<< HEAD
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ApplicationDbContext _context;

        public AuthController(IAuthService service, IConfiguration configuration,SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
=======
        //        public AuthController(IAuthService service)
        public AuthController(IAuthService service, IConfiguration configuration)
>>>>>>> 5214b57e3f10b832105456c72dacff5b1de60d2b
        {
            this._service = service;
            _configuration = configuration;
            _varConnStr = _configuration.GetValue<string>("ConnectionStrings:DBConnectionString");
<<<<<<< HEAD
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
            _context = context;
=======
>>>>>>> 5214b57e3f10b832105456c72dacff5b1de60d2b
        }

        [HttpPost]
        public async Task<IActionResult> Registration(AccountRegistration model)
        {
            if (!ModelState.IsValid)
                return View(model);
            model.Role = "user";
            var result = await _service.RegistrationAsync(model);
            TempData["msg"] = result.Message;
            return RedirectToAction(nameof(Registration));
        }
        public IActionResult Login()
        {
            //return View("~/Views/Main/Index.cshtml");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
         {
<<<<<<< HEAD
            var status = new Status();
            if (model.Email != null && model.Password != null)
            {
                var result = await _service.LoginAsync(model);
                if (result.StatusCode == 1)
                {
                    // Crear la conexión a la base de datos
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
                                command.Parameters.AddWithValue("@Email", model.Email);
                                // Ejecutar el stored procedure y leer los resultados
                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        bool lockoutEnabled = (bool)reader["LockoutEnabled"];
                                        bool isNew = (bool)reader["IsNew"];
                                        if (lockoutEnabled == true || isNew == true) //-> redirija a la vista de cambio de password para que al cambiarla, se desbloquee (pase lockoutEnabled a false y isNew a false)
                                        {
                                            // Obtener el objeto de sesión
                                            ISession session = HttpContext.Session;
                                            //Establecer el valor en la sesión
                                            session.SetString("EmailSession", model.Email);
                                            connection.Close();
                                            return RedirectToAction("Cambio_password", "Auth");
                                        }
                                        else
                                        {
                                            // Obtener el objeto de sesión
                                            ISession session = HttpContext.Session;
                                            //Establecer el valor en la sesión
                                            session.SetString("EmailSession", model.Email);
                                            connection.Close();
                                            return RedirectToAction("Index", "Main");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("No se encontraron resultados.");
                                        connection.Close();
                                        return View();
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                            connection.Close();
                            return View();
                        }
                    }
                }else{
                    TempData["msg"] = result.Message;
                    ModelState.SetModelValue("Email", new ValueProviderResult(model.Email, CultureInfo.InvariantCulture));
                    return View();
                }
            }else{
                status.Message = "Please enter your email and password";
                TempData["msg"] = status.Message;
                ModelState.SetModelValue("Email", new ValueProviderResult(model.Email, CultureInfo.InvariantCulture));
                return View();
=======
            /* if (!ModelState.IsValid)
             {
                 return View(model);
             }
             var result = await _service.LoginAsync(model);
             if (result.StatusCode == 1)
             {
                 //if (cuenta es nueva?(columna)and cuenta inactiva?(columna)) -> redirija a la vista de cambio de password
                 //{
                 // return RedirectToAction("Cambio_password", "Auth");
                 //}else{
                     return RedirectToAction("Index", "Main");
                 //}
             }
             else
             {
                 TempData["msg"] = result.Message;
                 return RedirectToAction(nameof(Login));
             }*/
            /*if (!ModelState.IsValid)
            {
                return View(model);
            }*/
            var result = await _service.LoginAsync(model);
            if (result.StatusCode == 1)
            {
                // Crear la conexión a la base de datos
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
                            command.Parameters.AddWithValue("@Email", model.Email);
                            // Ejecutar el stored procedure y leer los resultados
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    bool lockoutEnabled = (bool)reader["LockoutEnabled"];
                                    bool isNew = (bool)reader["IsNew"];
                                    if (lockoutEnabled == true || isNew == true) //-> redirija a la vista de cambio de password para que al cambiarla, se desbloquee (pase lockoutEnabled a false y isNew a false)
                                    {
                                        // Obtener el objeto de sesión
                                        ISession session = HttpContext.Session;
                                        //Establecer el valor en la sesión
                                        session.SetString("EmailSession", model.Email);
                                        return RedirectToAction("Cambio_password", "Auth");
                                    }
                                    else
                                    {
                                        // Obtener el objeto de sesión
                                        ISession session = HttpContext.Session;
                                        //Establecer el valor en la sesión
                                        session.SetString("EmailSession", model.Email);

                                        return RedirectToAction("Index", "Main");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("No se encontraron resultados.");
                                    return RedirectToAction(nameof(Login));
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                        connection.Close();
                        return RedirectToAction(nameof(Login));
                    }
                }
            }
            else
            {
                TempData["msg"] = result.Message;
                return RedirectToAction(nameof(Login));
>>>>>>> 5214b57e3f10b832105456c72dacff5b1de60d2b
            }
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _service.LogoutAsync();
            return RedirectToAction(nameof(Login));
        }
        //comentar cuando ya no es necesario
        //Metodo para registrar cuentas y roles en tablas AspNetUsers, AspNetUserRoles y AspNetRoles por medio del navegador sin necesidad de crear un form
        //ejecutar agregando esto a la url: localhost:<num_puerto>/<nombrecontroller>/<metodo>,
        //i.e: localhost:878787/Auth/AddAccount 
        public async Task<IActionResult> AddAccount()
        {
            var model = new AccountRegistration
            {
<<<<<<< HEAD
                Username = "Melany",
                Name = "Melany Quesada",
                Email = "melany.quesada@gmail.com",
                Password = "Admin@12345#"
            };
            model.Role = "comun";
            var result = await _service.RegistrationAsync(model);
            return Ok(result);
        }

        /* metodo para actualizar cuenta  
         public async Task<IActionResult> UpdatePassword()
=======
                Username = "Lupita",
                Name = "Lupita Mendez",
                Email = "lupita.mendez@gmail.com",
                Password = "Admin@12345#"
            };
            model.Role = "admin";
            var result = await _service.RegistrationAsync(model);
            return Ok(result);
        }
        
        /* public async Task<IActionResult> UpdatePassword()
>>>>>>> 5214b57e3f10b832105456c72dacff5b1de60d2b
         {
             var model = new LoginModel
             {
                 Email = "jorge.granados@gmail.com",
                 Password = "Jorge@12345#"
             };
             var result = await _service.UpdateAsync(model);
             return Ok(result);
         } */
        public IActionResult Recuperar_password()
        {
            //mandar correo, pero por detras, antes de enviar el correo, el password tuvo que haberse actualizado, despues, se pasa a la vista Cambio_password que llama al metodo cambio_password
            return View();
        }
<<<<<<< HEAD
        [HttpPost]
        public async Task<IActionResult> Recuperar_password(string email)
        {
            var status = new Status();
            //mandar correo, pero por detras, antes de enviar el correo, el password tuvo que haberse actualizado, despues, se pasa a la vista Cambio_password que llama al metodo cambio_password
            LoginModel model = new LoginModel
            {
                Email = email
            };
            if (model.Email != null)
            {
                var check_email = await userManager.FindByEmailAsync(model.Email);
                if (check_email == null)
                {
                    status.StatusCode = 0;
                    status.Message = "Email has not been registered";
                    TempData["msg"] = status.Message;
                    return View();
                } else{
                    var result = await _service.SendResetEmail(model);
                    if (result.StatusCode == 1)
                    {
                        using (SqlConnection connection = new SqlConnection(_varConnStr))
                        {
                            try
                            {
                                connection.Open();
                                using (SqlCommand command_update = new SqlCommand("ChangeIsNew", connection))
                                {
                                    command_update.CommandType = CommandType.StoredProcedure;
                                    command_update.Parameters.AddWithValue("@email", model.Email);
                                    command_update.Parameters.AddWithValue("@newValue", 1);
                                    command_update.ExecuteNonQuery();
                                    connection.Close();
                                    status.StatusCode = 1;
                                    status.Message = "Now try log in with the new password that we sent to your email";
                                    TempData["msg"] = status.Message;
                                    ModelState.SetModelValue("Email", new ValueProviderResult(model.Email, CultureInfo.InvariantCulture));
                                    return RedirectToAction("Login", "Auth");
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");
                                status.Message = "An error has happened: " + ex.Message;
                                TempData["msg"] = result.Message;
                                connection.Close();
                                ModelState.SetModelValue("Email", new ValueProviderResult(model.Email, CultureInfo.InvariantCulture));
                                return View();
                            }
                        }
                    }
                    else
                    {
                        TempData["msg"] = result.Message;
                        ModelState.SetModelValue("Email", new ValueProviderResult(model.Email, CultureInfo.InvariantCulture));
                        return RedirectToAction("Login", "Auth");
                    }
                }
            }
            else
            {
                status.Message = "Please enter your email";
                TempData["msg"] = status.Message;
                return RedirectToAction(nameof(Recuperar_password));
            }
        }
=======
>>>>>>> 5214b57e3f10b832105456c72dacff5b1de60d2b
        [Authorize]
        public IActionResult Cambio_password()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Cambio_password(string email, string password, string oldpassword, string confirmpassword)
        {
<<<<<<< HEAD
            var status = new Status();
=======
>>>>>>> 5214b57e3f10b832105456c72dacff5b1de60d2b
            var model = new LoginModel
            {
                Email = email,
                OldPassword = oldpassword,
                Password = password,
                ConfirmPassword = confirmpassword
            };
<<<<<<< HEAD
            if (model.Email != null && model.Password != null && model.OldPassword != null && model.ConfirmPassword != null)
            {
                var result = await _service.UpdatePasswordAsync(model);
                if (result.StatusCode == 1)
                {   // Crear la conexión a la base de datos
                    using (SqlConnection connection = new SqlConnection(_varConnStr))
                    {
                        try
                        {
                            connection.Open();
                            using (SqlCommand command_update = new SqlCommand("ChangeIsNew", connection))
                            {
                                command_update.CommandType = CommandType.StoredProcedure;
                                command_update.Parameters.AddWithValue("@email", model.Email);
                                command_update.Parameters.AddWithValue("@newValue", 0);
                                command_update.ExecuteNonQuery();
                                connection.Close();
                                return RedirectToAction("Index", "Main");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                            connection.Close();
                            status.Message = "An unhandled error happened: " + ex.Message;
                            TempData["msg"] = status.Message;
                            return View();
                        }
                    }
                }
                else
                {
                    status.Message = "Passwords does not match";
                    TempData["msg"] = status.Message;
                    ModelState.SetModelValue("Email", new ValueProviderResult(model.Email, CultureInfo.InvariantCulture));
                    return View();
=======
           /* if (!ModelState.IsValid)
            {
                return View(model);
            }*/
            var result = await _service.UpdatePasswordAsync(model);
            if (result.StatusCode == 1)
            {   // Crear la conexión a la base de datos
                using (SqlConnection connection = new SqlConnection(_varConnStr))
                {
                    try
                    {
                        connection.Open();
                        using (SqlCommand command_update = new SqlCommand("ChangeIsNew", connection))
                        {
                            command_update.CommandType = CommandType.StoredProcedure;
                            command_update.Parameters.AddWithValue("@email", model.Email);
                            command_update.Parameters.AddWithValue("@newValue", 0);
                            command_update.ExecuteNonQuery();
                            return RedirectToAction("Index", "Main");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                        connection.Close();
                        return RedirectToAction(nameof(Cambio_password));
                    }
>>>>>>> 5214b57e3f10b832105456c72dacff5b1de60d2b
                }
            }
            else
            {
<<<<<<< HEAD
                status.Message = "Please complete the requested information";
                TempData["msg"] = status.Message;
                ModelState.SetModelValue("Email", new ValueProviderResult(model.Email, CultureInfo.InvariantCulture));
                return View();
            }
            
=======
                TempData["msg"] = result.Message;
                return RedirectToAction(nameof(Cambio_password));
            }
>>>>>>> 5214b57e3f10b832105456c72dacff5b1de60d2b
        }
    }
}
