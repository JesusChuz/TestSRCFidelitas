﻿using Microsoft.AspNetCore.Authorization;
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
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;
using Newtonsoft.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Text.RegularExpressions;
using MessagePack;
using Microsoft.VisualBasic;

namespace sistema_reconocimiento.Controllers
{
    public class MainController : Controller
    {
        private readonly INotificationEmailService _service;
        private readonly IAuthService _serviceAuth;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<MainController> _logger;
        private readonly IConfiguration _configuration;
        private readonly string _varConnStr;
        public MainController(INotificationEmailService service, IAuthService serviceAuth, ILogger<MainController> logger, IConfiguration configuration, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this._service = service;
            _serviceAuth = serviceAuth;
            _context = context;
            _logger = logger;
            _userManager = userManager;
            _configuration = configuration;
            _varConnStr = _configuration.GetValue<string>("ConnectionStrings:DBConnectionString");
            _userManager = userManager;
        }
        public bool validateAccountEnabled(bool result)
        {
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
        public void LoadPoints(Engineers model)
        {
            using (SqlConnection connection = new SqlConnection(_varConnStr))
            {
                try
                {
                    connection.Open();
                    // Crear el comando y asignar el stored procedure
                    using (SqlCommand command = new SqlCommand("ShowPoints", connection))
                    {
                        // Obtener el objeto de sesión
                        ISession session = HttpContext.Session;
                        //Establecer el valor en la sesión
                        string email = session.GetString("EmailSession");
                        command.CommandType = CommandType.StoredProcedure;
                        // Agregar parámetro de entrada
                        command.Parameters.AddWithValue("@email", email);
                        // Ejecutar el stored procedure y leer los resultados
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                model.Points = (int)reader["Points"];
                                ViewBag.GetPoints = model.Points;
                            }
                            connection.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    connection.Close();
                }
            }
        }
        public void LoadRecognizedPoints(Engineers model, SubmitStateRecognition recognitions)
        {
            using (SqlConnection connection = new SqlConnection(_varConnStr))
            {
                try
                {
                    connection.Open();
                    // Crear el comando y asignar el stored procedure
                    using (SqlCommand command = new SqlCommand("ShowPoints", connection))
                    {
                        //Establecer el valor en la sesión
                        string email = recognitions.RecognitionsPOST.Recognized_EngEmail;
                        command.CommandType = CommandType.StoredProcedure;
                        // Agregar parámetro de entrada
                        command.Parameters.AddWithValue("@email", email);
                        // Ejecutar el stored procedure y leer los resultados
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                model.Points = (int)reader["Points"];
                                ViewBag.GetPointsRecognized = model.Points;
                            }
                            connection.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    connection.Close();
                }
            }
        }
        public void LoadIdEngineer(Engineers model)
        {
            using (SqlConnection connection = new SqlConnection(_varConnStr))
            {
                try
                {
                    connection.Open();
                    // Crear el comando y asignar el stored procedure
                    using (SqlCommand command = new SqlCommand("ShowIdEngineer", connection))
                    {
                        // Obtener el objeto de sesión
                        ISession session = HttpContext.Session;
                        //Establecer el valor en la sesión
                        string email = session.GetString("EmailSession");
                        command.CommandType = CommandType.StoredProcedure;
                        // Agregar parámetro de entrada
                        command.Parameters.AddWithValue("@email", email);
                        // Ejecutar el stored procedure y leer los resultados
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                model.ID_Engineer = (int)reader["ID_Engineer"];
                                ViewBag.GetIdEngineer = model.ID_Engineer;
                            }
                            connection.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    connection.Close();
                }
            }
        }
        public async Task<List<Rewards>> LoadRewards()
        {
            List<Rewards> rewards = await _context.Rewards.Where(e => e.IsEnabled == true).ToListAsync();

            return rewards;
        }
        [Authorize]
        public async Task<IActionResult> Index(bool result, Engineers model)
        {
            if (result == false) {
                result = validateAccountEnabled(result);
                if (result == true)
                {
                    LoadPoints(model);
                    LoadIdEngineer(model);
                    var applicationDbContext = _context.Rewards;
                    var viewModel = new SubmitPurchaseViewModel
                    {
                        Purchases = new Purchases(),
                        Rewards = await LoadRewards()
                    };
                   
                   // var applicationDbContext = _context.Purchases.Include(e => e.Rewards);

                    //return View(await applicationDbContext.ToListAsync());
                    return View(viewModel);

                }
                else
                {
                    return RedirectToAction("Login", "Auth");
                }
            }
            return RedirectToAction("Login", "Auth");
        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> IngenierosAsync(bool result, Engineers model)
        {
            if (result == false)
            {
                result = validateAccountEnabled(result);
                if (result == true)
                {
                    LoadPoints(model);
                    LoadIdEngineer(model);
                    var applicationDbContext = _context.Engineers.Include(e => e.ApplicationUser).Include(e => e.Manager).Include(e => e.Positions);
                    foreach (var engineer in applicationDbContext)
                    {
                        var user = await _userManager.FindByIdAsync(engineer.ApplicationUser.Id);
                        var roles = await _userManager.GetRolesAsync(user);
                        // Aquí podrías asignar los roles a un nuevo campo en tu modelo, por ejemplo:
                        engineer.ApplicationUser.Roles = (List<string>)roles;
                    }
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
        public IActionResult Agregar_ingeniero(bool result, Engineers model)
        {
            if (result == false)
            {
                result = validateAccountEnabled(result);
                if (result == true)
                {
                    LoadPoints(model);
                    LoadIdEngineer(model);
                    var loadmanager = _context.Set<Manager>()
                    .Select(m => new
                    {
                        ID_Manager = m.ID_Manager,
                        FullName = m.Name_Manager + " " + m.LastName_Manager + " (" + m.Email + ")"
                    })
                    .ToList();

                    ViewData["ID_Account"] = new SelectList(_context.ApplicationUser, "Id", "Id");
                    ViewData["ID_Manager"] = new SelectList(loadmanager, "ID_Manager", "FullName");
                    ViewData["Position"] = new SelectList(_context.Positions, "ID_Position", "Position_Name");

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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Agregar_ingeniero([Bind("ID_Engineer,Name_Engineer,LastName_Engineer,Position,Points,ID_Account,ID_Manager")] Engineers engineers, string UserRole, string Email, string Password, string ConfirmPassword)
        {
            LoadPoints(engineers);
            var loadmanager = _context.Set<Manager>()
            .Select(m => new
            {
                ID_Manager = m.ID_Manager,
                FullName = m.Name_Manager + " " + m.LastName_Manager + " (" + m.Email + ")"
            })
            .ToList();
            var status = new Status();
            try
            {
                if (string.IsNullOrWhiteSpace(engineers.Name_Engineer) || engineers.Name_Engineer.Length < 2 || engineers.Name_Engineer.Length > 30)
                {
                    status.Message = "The name of the engineer is required and should be between 2 and 30 characters long";
                    TempData["msgName_Engineer"] = status.Message;

                    ViewData["ID_Account"] = new SelectList(_context.ApplicationUser, "Id", "Id", engineers.ID_Account);
                    ViewData["ID_Manager"] = new SelectList(loadmanager, "ID_Manager", "FullName", engineers.ID_Manager);
                    ViewData["Position"] = new SelectList(_context.Positions, "ID_Position", "Position_Name", engineers.Position);
                    return View(engineers);
                }
                if (_context.ApplicationUser.Any(u => u.Email == Email))
                {

                    status.Message = "The email has been registered already";
                    TempData["msgEmail"] = status.Message;

                    ViewData["ID_Account"] = new SelectList(_context.ApplicationUser, "Id", "Id", engineers.ID_Account);
                    ViewData["ID_Manager"] = new SelectList(loadmanager, "ID_Manager", "FullName", engineers.ID_Manager);
                    ViewData["Position"] = new SelectList(_context.Positions, "ID_Position", "Position_Name", engineers.Position);
                    return View(engineers);
                }
                if (string.IsNullOrWhiteSpace(engineers.LastName_Engineer) || engineers.LastName_Engineer.Length < 2 || engineers.LastName_Engineer.Length > 30)
                {

                    status.Message = "The last name of the engineer is required and should be between 2 and 30 characters long";
                    TempData["msgLastName_Engineer"] = status.Message;

                    ViewData["ID_Account"] = new SelectList(_context.ApplicationUser, "Id", "Id", engineers.ID_Account);
                    ViewData["ID_Manager"] = new SelectList(loadmanager, "ID_Manager", "FullName", engineers.ID_Manager);
                    ViewData["Position"] = new SelectList(_context.Positions, "ID_Position", "Position_Name", engineers.Position);
                    return View(engineers);
                }
                if (string.IsNullOrWhiteSpace(Email))
                {
                    status.Message = "Email is required";
                    TempData["msgEmail"] = status.Message;

                    ViewData["ID_Account"] = new SelectList(_context.ApplicationUser, "Id", "Id", engineers.ID_Account);
                    ViewData["ID_Manager"] = new SelectList(loadmanager, "ID_Manager", "FullName", engineers.ID_Manager);
                    ViewData["Position"] = new SelectList(_context.Positions, "ID_Position", "Position_Name", engineers.Position);
                    return View(engineers);
                }
                if (string.IsNullOrWhiteSpace(Password) || Password.Length < 6 || !Regex.IsMatch(Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\W).{6,}$"))
                {
                   
                    status.Message = "Password is required, needs to be at least 6 characters and you need to use a Capital letter, a lowercase, a symbol and a number";
                    TempData["msgPassword"] = status.Message;
                    
                    ViewData["ID_Account"] = new SelectList(_context.ApplicationUser, "Id", "Id", engineers.ID_Account);
                    ViewData["ID_Manager"] = new SelectList(loadmanager, "ID_Manager", "FullName", engineers.ID_Manager);
                    ViewData["Position"] = new SelectList(_context.Positions, "ID_Position", "Position_Name", engineers.Position);
                    return View(engineers);
                }
                if (Password != ConfirmPassword)
                {
                    status.Message = "Verify that you confirmed the password";
                    TempData["msgPasswordC"] = status.Message;

                    ViewData["ID_Account"] = new SelectList(_context.ApplicationUser, "Id", "Id", engineers.ID_Account);
                    ViewData["ID_Manager"] = new SelectList(loadmanager, "ID_Manager", "FullName", engineers.ID_Manager);
                    ViewData["Position"] = new SelectList(_context.Positions, "ID_Position", "Position_Name", engineers.Position);
                    return View(engineers);
                }
                var emailAttribute = new EmailAddressAttribute();
                if (!emailAttribute.IsValid(Email))
                {
                   
                    status.Message = "The email format is invalid";
                    TempData["msgEmail"] = status.Message;

                    ViewData["ID_Account"] = new SelectList(_context.ApplicationUser, "Id", "Id", engineers.ID_Account);
                    ViewData["ID_Manager"] = new SelectList(loadmanager, "ID_Manager", "FullName", engineers.ID_Manager);
                    ViewData["Position"] = new SelectList(_context.Positions, "ID_Position", "Position_Name", engineers.Position);
                    return View(engineers);
                }
                if (engineers.Position <= 0)
                {
                  
                    status.Message = "Select a position";
                    TempData["msgPosition"] = status.Message;

                    ViewData["ID_Account"] = new SelectList(_context.ApplicationUser, "Id", "Id", engineers.ID_Account);
                    ViewData["ID_Manager"] = new SelectList(loadmanager, "ID_Manager", "FullName", engineers.ID_Manager);
                    ViewData["Position"] = new SelectList(_context.Positions, "ID_Position", "Position_Name", engineers.Position);
                    return View(engineers);
                }

                if (engineers.ID_Manager <= 0)
                {
                   
                    status.Message = "Select a manager";
                    TempData["msgManager"] = status.Message;

                    ViewData["ID_Account"] = new SelectList(_context.ApplicationUser, "Id", "Id", engineers.ID_Account);
                    ViewData["ID_Manager"] = new SelectList(loadmanager, "ID_Manager", "FullName", engineers.ID_Manager);
                    ViewData["Position"] = new SelectList(_context.Positions, "ID_Position", "Position_Name", engineers.Position);
                    return View(engineers);
                }
                if (string.IsNullOrWhiteSpace(UserRole) || (UserRole != "comun" && UserRole != "admin"))
                {
                    status.Message = "Select a role for the user";
                    TempData["msgRole"] = status.Message;

                    ViewData["ID_Account"] = new SelectList(_context.ApplicationUser, "Id", "Id", engineers.ID_Account);
                    ViewData["ID_Manager"] = new SelectList(loadmanager, "ID_Manager", "FullName", engineers.ID_Manager);
                    ViewData["Position"] = new SelectList(_context.Positions, "ID_Position", "Position_Name", engineers.Position);
                    return View(engineers);
                }
                var model = new AccountRegistration
                {
                    Username = engineers.Name_Engineer,
                    Name = engineers.Name_Engineer + "  " + engineers.LastName_Engineer,
                    Email = Email,
                    Password = (string)(TempData["Password"] = Password),
                    Role = UserRole,
                    IsNew = true
                };

                var accountResult = await _serviceAuth.RegistrationAsync(model);
                engineers.ID_Account = accountResult.AccountId; // Asigna el ID de la cuenta recién creada.
                engineers.Points = 0;

                _context.Add(engineers);
                await _context.SaveChangesAsync();
                TempData["IngCreado"] = true;
                return RedirectToAction("Ingenieros", "Main");
            }
            catch
            {
                ViewData["ID_Account"] = new SelectList(_context.ApplicationUser, "Id", "Id", engineers.ID_Account);
                ViewData["ID_Manager"] = new SelectList(loadmanager, "ID_Manager", "FullName", engineers.ID_Manager);
                ViewData["Position"] = new SelectList(_context.Positions, "ID_Position", "Position_Name", engineers.Position);
                return RedirectToAction("Index", "Main");
            }
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Editar_ingeniero(int? id, Engineers model)
        {
            LoadPoints(model);
            if (id == null || _context.Engineers == null)
            {
                return NotFound();
            }

            var engineers = await _context.Engineers.FindAsync(id);
            if (engineers == null)
            {
                return NotFound();
            }

            var applicationUser = await _userManager.FindByIdAsync(engineers.ID_Account);

            if (applicationUser == null)
            {
                return NotFound();
            }

            // Pasar el email al ViewData.
            ViewData["Email"] = applicationUser.Email;

            var user = await _userManager.FindByIdAsync(engineers.ID_Account);

            // Obtener el rol del usuario
            var roles = await _userManager.GetRolesAsync(user);

            // Asignar el rol al ViewData
            if (roles.Count > 0)
            {
                ViewData["Rol"] = roles[0];
            }
            else
            {
                ViewData["Rol"] = "Not assigned";
            }

            var loadmanager = _context.Set<Manager>()
            .Select(m => new
            {
                ID_Manager = m.ID_Manager,
                FullName = m.Name_Manager + " " + m.LastName_Manager + " (" + m.Email + ")"
            })
            .ToList();

            ViewData["ID_Account"] = new SelectList(_context.ApplicationUser, "Id", "Id");
            ViewData["ID_Manager"] = new SelectList(loadmanager, "ID_Manager", "FullName");
            ViewData["Position"] = new SelectList(_context.Positions, "ID_Position", "Position_Name");
            return View(engineers);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar_ingeniero(int id, [Bind("ID_Engineer,Name_Engineer,LastName_Engineer,Position,ID_Account,ID_Manager")] Engineers engineers, string Email, string Password, string ConfirmPassword, string UserRole)
        {
            LoadPoints(engineers);
            var loadmanager = _context.Set<Manager>()
            .Select(m => new
            {
                ID_Manager = m.ID_Manager,
                FullName = m.Name_Manager + " " + m.LastName_Manager + " (" + m.Email + ")"
            })
            .ToList();
            var status = new Status();
            try
            {
                if (string.IsNullOrWhiteSpace(engineers.Name_Engineer) || engineers.Name_Engineer.Length < 2 || engineers.Name_Engineer.Length > 30)
                {
                    status.Message = "The name of the engineer is required and should be between 2 and 30 characters long";
                    TempData["msgName_Engineer"] = status.Message;

                    ViewData["ID_Account"] = new SelectList(_context.ApplicationUser, "Id", "Id", engineers.ID_Account);
                    ViewData["ID_Manager"] = new SelectList(loadmanager, "ID_Manager", "FullName", engineers.ID_Manager);
                    ViewData["Position"] = new SelectList(_context.Positions, "ID_Position", "Position_Name", engineers.Position);
                    return View(engineers);
                }
                if (string.IsNullOrWhiteSpace(engineers.LastName_Engineer) || engineers.LastName_Engineer.Length < 2 || engineers.LastName_Engineer.Length > 30)
                {

                    status.Message = "The last name of the engineer is required and should be between 2 and 30 characters long";
                    TempData["msgLastName_Engineer"] = status.Message;

                    ViewData["ID_Account"] = new SelectList(_context.ApplicationUser, "Id", "Id", engineers.ID_Account);
                    ViewData["ID_Manager"] = new SelectList(loadmanager, "ID_Manager", "FullName", engineers.ID_Manager);
                    ViewData["Position"] = new SelectList(_context.Positions, "ID_Position", "Position_Name", engineers.Position);
                    return View(engineers);
                }
                if (string.IsNullOrWhiteSpace(Email))
                {
                    status.Message = "Email is required";
                    TempData["msgEmail"] = status.Message;

                    ViewData["ID_Account"] = new SelectList(_context.ApplicationUser, "Id", "Id", engineers.ID_Account);
                    ViewData["ID_Manager"] = new SelectList(loadmanager, "ID_Manager", "FullName", engineers.ID_Manager);
                    ViewData["Position"] = new SelectList(_context.Positions, "ID_Position", "Position_Name", engineers.Position);
                    return View(engineers);
                }
                if (string.IsNullOrWhiteSpace(Password) || Password.Length < 6 || !Regex.IsMatch(Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\W).{6,}$"))
                {

                    status.Message = "Password is required, needs to be at least 6 characters and you need to use a Capital letter, a lowercase, a symbol and a number";
                    TempData["msgPassword"] = status.Message;

                    ViewData["ID_Account"] = new SelectList(_context.ApplicationUser, "Id", "Id", engineers.ID_Account);
                    ViewData["ID_Manager"] = new SelectList(loadmanager, "ID_Manager", "FullName", engineers.ID_Manager);
                    ViewData["Position"] = new SelectList(_context.Positions, "ID_Position", "Position_Name", engineers.Position);
                    return View(engineers);
                }
                if (Password != ConfirmPassword)
                {
                    status.Message = "Verify that is the same password";
                    TempData["msgPasswordC"] = status.Message;

                    ViewData["ID_Account"] = new SelectList(_context.ApplicationUser, "Id", "Id", engineers.ID_Account);
                    ViewData["ID_Manager"] = new SelectList(loadmanager, "ID_Manager", "FullName", engineers.ID_Manager);
                    ViewData["Position"] = new SelectList(_context.Positions, "ID_Position", "Position_Name", engineers.Position);
                    return View(engineers);
                }
                var emailAttribute = new EmailAddressAttribute();
                if (!emailAttribute.IsValid(Email))
                {

                    status.Message = "The email format is invalid";
                    TempData["msgEmail"] = status.Message;

                    ViewData["ID_Account"] = new SelectList(_context.ApplicationUser, "Id", "Id", engineers.ID_Account);
                    ViewData["ID_Manager"] = new SelectList(loadmanager, "ID_Manager", "FullName", engineers.ID_Manager);
                    ViewData["Position"] = new SelectList(_context.Positions, "ID_Position", "Position_Name", engineers.Position);
                    return View(engineers);
                }
                if (engineers.Position <= 0)
                {

                    status.Message = "Select a position";
                    TempData["msgPosition"] = status.Message;

                    ViewData["ID_Account"] = new SelectList(_context.ApplicationUser, "Id", "Id", engineers.ID_Account);
                    ViewData["ID_Manager"] = new SelectList(loadmanager, "ID_Manager", "FullName", engineers.ID_Manager);
                    ViewData["Position"] = new SelectList(_context.Positions, "ID_Position", "Position_Name", engineers.Position);
                    return View(engineers);
                }

                if (engineers.ID_Manager <= 0)
                {

                    status.Message = "Select a manager";
                    TempData["msgManager"] = status.Message;

                    ViewData["ID_Account"] = new SelectList(_context.ApplicationUser, "Id", "Id", engineers.ID_Account);
                    ViewData["ID_Manager"] = new SelectList(loadmanager, "ID_Manager", "FullName", engineers.ID_Manager);
                    ViewData["Position"] = new SelectList(_context.Positions, "ID_Position", "Position_Name", engineers.Position);
                    return View(engineers);
                }
                if (string.IsNullOrWhiteSpace(UserRole) || (UserRole != "comun" && UserRole != "admin"))
                {
                    status.Message = "Select a role for the user";
                    TempData["msgRole"] = status.Message;

                    ViewData["ID_Account"] = new SelectList(_context.ApplicationUser, "Id", "Id", engineers.ID_Account);
                    ViewData["ID_Manager"] = new SelectList(loadmanager, "ID_Manager", "FullName", engineers.ID_Manager);
                    ViewData["Position"] = new SelectList(_context.Positions, "ID_Position", "Position_Name", engineers.Position);
                    return View(engineers);
                }

                // Cargar el ApplicationUser relacionado.
                var applicationUser = await _userManager.FindByIdAsync(engineers.ID_Account);

                // Modificar las propiedades.
                applicationUser.Name = engineers.Name_Engineer + "  " + engineers.LastName_Engineer;
                applicationUser.UserName = engineers.Name_Engineer;
                applicationUser.Email = Email;
                applicationUser.PasswordHash = _userManager.PasswordHasher.HashPassword(applicationUser, Password);

                // Actualizar el ApplicationUser en la base de datos.
                var updateResult = await _userManager.UpdateAsync(applicationUser);

                // Asegúrar de que el rol existe.
                var user = await _userManager.FindByIdAsync(engineers.ID_Account);

                if (user == null)
                {
                    return NotFound();
                }

                var roles = await _userManager.GetRolesAsync(user);
                if (roles.Any())
                {
                    // Eliminar todos los roles existentes.
                    var removeResult = await _userManager.RemoveFromRolesAsync(user, roles);
                    if (!removeResult.Succeeded)
                    {
                        // Manejar los errores.
                        foreach (var error in removeResult.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                        return View(engineers);
                    }
                }
                // Añadir el nuevo rol.
                var addResult = await _userManager.AddToRoleAsync(user, UserRole);

                _context.Update(engineers);
                await _context.SaveChangesAsync();
                TempData["IngModif"] = true;
                return RedirectToAction("Ingenieros", "Main");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EngineersExists(engineers.ID_Engineer))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        private bool EngineersExists(int iD_Engineer)
        {
            throw new NotImplementedException();
        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Eliminar_Ingeniero(int? id)
        {
            var status = new Status();
            if (id == null || _context.Engineers == null)
            {
                return NotFound();
            }

            var engineers = await _context.Engineers
                .Include(e => e.ApplicationUser)
                .Include(e => e.Manager)
                .Include(e => e.Positions)
                .FirstOrDefaultAsync(m => m.ID_Engineer == id);
            if (engineers == null)
            {
                return NotFound();
            }
            if (_context.Engineers == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Engineers'  is null.");
            }
            var engineer = await _context.Engineers.FindAsync(id);
            if (engineers != null)
            {
                _context.Engineers.Remove(engineers);
            }
            var user = await _userManager.FindByIdAsync(engineer.ID_Account);
            if (user == null)
            {
                return NotFound();
            }

            // Eliminar el usuario de la tabla AspNetUsers
            try
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    await _context.SaveChangesAsync();
                    TempData["IngEliminado"] = true;
                    return RedirectToAction("Ingenieros", "Main");
                }
                else
                {
                    TempData["msg"] = "An error happened trying to delete this engineer";
                    return RedirectToAction("Ingenieros", "Main");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Constraint problem when trying to delete this engineer, error: " +ex);
                using (SqlConnection connection = new SqlConnection(_varConnStr))
                {
                    connection.Open();
                    using (SqlCommand command_update = new SqlCommand("ChangeIsNew", connection))
                    {
                        command_update.CommandType = CommandType.StoredProcedure;
                        command_update.Parameters.AddWithValue("@email", user.Email);
                        command_update.Parameters.AddWithValue("@newValueIsNew", 0);
                        command_update.Parameters.AddWithValue("@newValueLockout", 1);
                        command_update.ExecuteNonQuery();
                        connection.Close();
                        TempData["msgEngineerConstraint"] = "True";
                        return RedirectToAction("Ingenieros", "Main");
                    }
                }
            }
        }
        public async Task<IActionResult> Change_lock(int? id) {
            var status = new Status();
            try
            {
                var engineers = await _context.Engineers
                .Include(e => e.ApplicationUser)
                .Include(e => e.Manager)
                .Include(e => e.Positions)
                .FirstOrDefaultAsync(m => m.ID_Engineer == id);
                if (engineers == null)
                {
                    return NotFound();
                }
                if (_context.Engineers == null)
                {
                    return Problem("Entity set 'ApplicationDbContext.Engineers'  is null.");
                }
                var engineer = await _context.Engineers.FindAsync(id);
                var user = await _userManager.FindByIdAsync(engineer.ID_Account);
                using (SqlConnection connection = new SqlConnection(_varConnStr))
                {
                    connection.Open();
                    // Crear el comando y asignar el stored procedure
                    using (SqlCommand command = new SqlCommand("GetLockoutEnabledAndIsNew", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        // Agregar parámetro de entrada
                        command.Parameters.AddWithValue("@Email", user.Email);
                        // Ejecutar el stored procedure y leer los resultados
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                bool lockoutEnabled = (bool)reader["LockoutEnabled"];
                                bool isNew = (bool)reader["IsNew"];
                                if (lockoutEnabled == true)
                                {
                                    using (SqlCommand command_update = new SqlCommand("ChangeIsNew", connection))
                                    {
                                        command_update.CommandType = CommandType.StoredProcedure;
                                        command_update.Parameters.AddWithValue("@email", user.Email);
                                        command_update.Parameters.AddWithValue("@newValueIsNew", 0);
                                        command_update.Parameters.AddWithValue("@newValueLockout", 0);
                                        command_update.ExecuteNonQuery();
                                        connection.Close();
                                        TempData["msgAccountEnabled"] = "True";
                                        return RedirectToAction("Ingenieros", "Main");
                                    }
                                }
                                else
                                {
                                    using (SqlCommand command_update = new SqlCommand("ChangeIsNew", connection))
                                    {
                                        command_update.CommandType = CommandType.StoredProcedure;
                                        command_update.Parameters.AddWithValue("@email", user.Email);
                                        command_update.Parameters.AddWithValue("@newValueIsNew", 0);
                                        command_update.Parameters.AddWithValue("@newValueLockout", 1);
                                        command_update.ExecuteNonQuery();
                                        connection.Close();
                                        TempData["msgAccountDisabled"] = "True";
                                        return RedirectToAction("Ingenieros", "Main");
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("No results found");
                                connection.Close();
                                return RedirectToAction("Ingenieros", "Main");
                            }
                        }
                    }
                }
            }catch(Exception ex){
                Console.WriteLine("An error occurred:" +ex);
                status.Message = ex.Message;
                TempData["msg"] = status.Message;
                return View();
            } 
        }
        public async Task<IActionResult> Change_RewardState(int? id)
        {
            var status = new Status();
            try
            {
                var rewards = _context.Rewards.FirstOrDefault(r => r.ID_Reward == id);
                using (SqlConnection connection = new SqlConnection(_varConnStr))
                {
                    connection.Open();
                    // Crear el comando y asignar el stored procedure
                    using (SqlCommand command = new SqlCommand("GetRewardState", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        // Agregar parámetro de entrada
                        command.Parameters.AddWithValue("@ID_Reward", rewards.ID_Reward);
                        // Ejecutar el stored procedure y leer los resultados
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                bool IsEnabled = (bool)reader["IsEnabled"];
                                if (IsEnabled == true)
                                {
                                    using (SqlCommand command_update = new SqlCommand("ChangeRewardState", connection))
                                    {
                                        command_update.CommandType = CommandType.StoredProcedure;
                                        command_update.Parameters.AddWithValue("@ID_Reward", rewards.ID_Reward);
                                        command_update.Parameters.AddWithValue("@newValue", 0);
                                        command_update.ExecuteNonQuery();
                                        connection.Close();
                                        TempData["msgRewardEnabled"] = "True";
                                        return RedirectToAction("Recompensas", "Main");
                                    }
                                }
                                else
                                {
                                    using (SqlCommand command_update = new SqlCommand("ChangeRewardState", connection))
                                    {
                                        command_update.CommandType = CommandType.StoredProcedure;
                                        command_update.Parameters.AddWithValue("@ID_Reward", rewards.ID_Reward);
                                        command_update.Parameters.AddWithValue("@newValue", 1);
                                        command_update.ExecuteNonQuery();
                                        connection.Close();
                                        TempData["msgRewardDisabled"] = "True";
                                        return RedirectToAction("Recompensas", "Main");
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("No results found");
                                connection.Close();
                                return RedirectToAction("Recompensas", "Main");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred:" + ex);
                status.Message = ex.Message;
                TempData["msg"] = status.Message;
                return View();
            }
        }
        public async Task<List<Recognitions>> LoadRecognitionsPending(String recognitionState)
        {
            List<Recognitions> recognitions = await _context.Recognitions
                .Include(e => e.PetitionerEngineer)
                .Include(e => e.RecognizedEngineer)
                .Where(e => e.Recognition_State == recognitionState)
                .ToListAsync();

            return recognitions;
        }
        public async Task<List<Recognitions>> LoadRecognitionsAR(String recognitionState)
        {
            List<Recognitions> recognitions = await _context.Recognitions
                .Include(e => e.PetitionerEngineer)
                .Include(e => e.RecognizedEngineer)
                .Where(e => e.Recognition_State != recognitionState)
                .ToListAsync();

            return recognitions;
        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Reconocimientos(bool result, Engineers modelE, SubmitStateRecognition model, int? page)
        {
            if (result == false)
            {
                result = validateAccountEnabled(result);
                if (result == true)
                {
                    LoadPoints(modelE);
                    LoadIdEngineer(modelE);
                    // LoadRecognizedPoints(modelE, model);
                    var viewModel = new SubmitStateRecognition
                    {
                        RecognitionsPOST = new Recognitions(),
                        RecognitionsP = await LoadRecognitionsPending("Pending"),
                        RecognitionsAR = await LoadRecognitionsAR("Pending")
                    };
                    return View(viewModel);
                }
                else
                {
                    return RedirectToAction("Login", "Auth");
                }
            }
            return RedirectToAction("Login", "Auth");
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Aprobar_Reconocimiento(bool result, Engineers engineers, SubmitStateRecognition strecognitions)
        {
            var status = new Status();
            var login = new LoginModel();
            var manager = new Manager();
            using (SqlConnection connection = new SqlConnection(_varConnStr))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command_update = new SqlCommand("UpdateRecognitionState", connection))
                    {
                        command_update.CommandType = CommandType.StoredProcedure;
                        command_update.Parameters.AddWithValue("@ID_Recognition", strecognitions.RecognitionsPOST.ID_Recognition);
                        command_update.Parameters.AddWithValue("@newState", "Approved");
                        command_update.ExecuteNonQuery();
                        strecognitions.RecognitionsPOST.Recognition_State = "Approved";
                        var resultNotification = await _service.SendStateRecognition(login, engineers, strecognitions, manager);
                        if (resultNotification.StatusCode == 1)
                        {
                            if (resultNotification.StatusCode == 1)
                            {
                                LoadRecognizedPoints(engineers, strecognitions);
                                status.StatusCode = 1;
                                int newPoints = engineers.Points + 100;
                                using (SqlCommand points_update = new SqlCommand("UpdatePoints", connection))
                                {
                                    points_update.CommandType = CommandType.StoredProcedure;
                                    points_update.Parameters.AddWithValue("@Engineer_ID", strecognitions.RecognitionsPOST.ID_EngineerRec);
                                    points_update.Parameters.AddWithValue("@newPoints", newPoints);
                                    points_update.ExecuteNonQuery();
                                    connection.Close();
                                }
                                status.Message = "The recognition has been approved";
                                TempData["RecogApproved"] = true;
                                return RedirectToAction("Reconocimientos", "Main");
                            }
                            else
                            {
                                TempData["msg"] = resultNotification.Message;
                                return RedirectToAction("Index", "Main");
                            }
                        }
                        else
                        {
                            TempData["msg"] = resultNotification.Message;
                            return RedirectToAction("Index", "Main");
                        }
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    connection.Close();
                    status.Message = "An unhandled error happened: " + ex.Message;
                    TempData["msg"] = status.Message;
                    return RedirectToAction("Reconocimientos", "Main");
                }
            }
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Rechazar_Reconocimiento(bool result, Engineers engineers, SubmitStateRecognition strecognitions)
        {
            var status = new Status();
            var login = new LoginModel();
            var manager = new Manager();
            using (SqlConnection connection = new SqlConnection(_varConnStr))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command_update = new SqlCommand("UpdateRecognitionState", connection))
                    {
                        command_update.CommandType = CommandType.StoredProcedure;
                        command_update.Parameters.AddWithValue("@ID_Recognition", strecognitions.RecognitionsPOST.ID_Recognition);
                        command_update.Parameters.AddWithValue("@newState", "Rejected");
                        command_update.ExecuteNonQuery();
                        connection.Close();
                        strecognitions.RecognitionsPOST.Recognition_State = "Rejected";
                        var resultNotification = await _service.SendStateRecognition(login, engineers, strecognitions, manager);
                        if (resultNotification.StatusCode == 1)
                        {

                            if (resultNotification.StatusCode == 1)
                            {
                                status.StatusCode = 1;
                                status.Message = "The recognition has been rejected";
                                TempData["RecogRejected"] = true;
                                return RedirectToAction("Reconocimientos", "Main");
                            }
                            else
                            {
                                TempData["msg"] = resultNotification.Message;
                                return RedirectToAction("Index", "Main");
                            }
                        }
                        else
                        {
                            TempData["msg"] = resultNotification.Message;
                            return RedirectToAction("Index", "Main");
                        }
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
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Eliminar_Reconocimiento(SubmitStateRecognition strecognitions)
        {
            var status = new Status();
            var login = new LoginModel();
            var manager = new Manager();
            if (_context.Recognitions == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Recognitions'  is null.");
            }
            var recognitions = await _context.Recognitions.FindAsync(strecognitions.RecognitionsPOST.ID_Recognition);
            if (recognitions != null)
            {
                _context.Recognitions.Remove(recognitions);
            }

            await _context.SaveChangesAsync();
            TempData["RecogDeleted"] = true;
            return RedirectToAction(nameof(Reconocimientos));
        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> RecompensasAsync(bool result, Engineers model)
        {
            if (result == false)
            {
                result = validateAccountEnabled(result);
                if (result == true)
                {
                    LoadPoints(model);
                    LoadIdEngineer(model);
                    List<Rewards> rewards = await _context.Rewards.ToListAsync();

                    return View(rewards);
                }
                else
                {
                    return RedirectToAction("Login", "Auth");
                }
            }
            return RedirectToAction("Login", "Auth");
        }
        [Authorize(Roles = "admin")]
        public IActionResult Agregar_recompensa(bool result, Engineers model)
        {
            if (result == false)
            {
                result = validateAccountEnabled(result);
                if (result == true)
                {
                    LoadPoints(model);
                    return View();
                }
                else
                {
                    return RedirectToAction("Login", "Auth");
                }
            }
            return RedirectToAction("Login", "Auth");
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Agregar_recompensa(Rewards rewards, Engineers model)
        {
            var status = new Status();
            LoadPoints(model);
            if (rewards.Reward_Name != null && rewards.Reward_Description != null && rewards.Price != 0 && rewards.PictureFile != null)
            {

                if (rewards.PictureFile != null && rewards.PictureFile.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        rewards.PictureFile.CopyTo(memoryStream);
                        rewards.Picture = memoryStream.ToArray();
                    }
                    _context.Add(rewards);
                    rewards.IsEnabled = true;
                    await _context.SaveChangesAsync();
                    TempData["RenCreado"] = true;
                    return RedirectToAction("Recompensas");
                }
                else
                {
                    status.Message = "You need to upload an image";
                    TempData["msgPictureFile"] = status.Message;
                    ModelState.SetModelValue("Reward_Name", new ValueProviderResult(rewards.Reward_Name, CultureInfo.InvariantCulture));
                    ModelState.SetModelValue("Reward_Description", new ValueProviderResult(rewards.Reward_Description, CultureInfo.InvariantCulture));
                    return View();
                }
            }
            if (rewards.Reward_Name == null)
            {
                status.Message = "Reward's name is required";
                TempData["msgReward_Name"] = status.Message;
                ModelState.SetModelValue("Reward_Name", new ValueProviderResult(rewards.Reward_Name, CultureInfo.InvariantCulture));
                ModelState.SetModelValue("Reward_Description", new ValueProviderResult(rewards.Reward_Description, CultureInfo.InvariantCulture));
                //ModelState.SetModelValue("Price", new ValueProviderResult(rewards.Price, CultureInfo.InvariantCulture)); -- tira error porque el tipo de dato es int, si se pasa a string deja de dar error
                return View();
            }
            if (rewards.Reward_Description == null)
            {
                status.Message = "Reward's description is required";
                TempData["msgReward_Description"] = status.Message;
                ModelState.SetModelValue("Reward_Name", new ValueProviderResult(rewards.Reward_Name, CultureInfo.InvariantCulture));
                ModelState.SetModelValue("Reward_Description", new ValueProviderResult(rewards.Reward_Description, CultureInfo.InvariantCulture));
                //ModelState.SetModelValue("Price", new ValueProviderResult(rewards.Price, CultureInfo.InvariantCulture)); -- tira error porque el tipo de dato es int, si se pasa a string deja de dar error
                return View();
            } 
            if (rewards.Price == 0)
            {
                status.Message = "Reward's price is required";
                TempData["msgPrice"] = status.Message;
                ModelState.SetModelValue("Reward_Name", new ValueProviderResult(rewards.Reward_Name, CultureInfo.InvariantCulture));
                ModelState.SetModelValue("Reward_Description", new ValueProviderResult(rewards.Reward_Description, CultureInfo.InvariantCulture));
                //ModelState.SetModelValue("Price", new ValueProviderResult(rewards.Price, CultureInfo.InvariantCulture)); -- tira error porque el tipo de dato es int, si se pasa a string deja de dar error
                return View();
            }
            return View();
        }
        public ActionResult ShowPicture(int id)
        {
            var reward = _context.Rewards.FirstOrDefault(i => i.ID_Reward == id);
            if (reward != null)
            {
                return File(reward.Picture, "image/png"); // Ajusta el tipo de contenido según el tipo de imagen que estés almacenando
            }
            return View();
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Editar_recompensa(bool result, Engineers model, int? id)
        {
            if (result == false)
            {
                result = validateAccountEnabled(result);
                if (result == true)
                {
                    LoadPoints(model);
                    if (id == null || _context.Rewards == null)
                    {
                        return NotFound();
                    }

                    var rewards = await _context.Rewards.FindAsync(id);

                    if (rewards == null)
                    {
                        return NotFound();
                    }
                    // Obtener la imagen actual desde la base de datos
                    byte[] imageData = rewards.Picture;

                    // Convertir los datos binarios de la imagen a una cadena Base64
                    string base64Image = imageData != null ? Convert.ToBase64String(imageData) : "";

                    // Asignar la cadena Base64 a una propiedad del modelo para mostrarla en la vista
                    rewards.Base64Image = base64Image;

                    return View(rewards);
                }
                else
                {
                    return RedirectToAction("Login", "Auth");
                }
            }
            return RedirectToAction("Login", "Auth");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar_recompensa(int id, [Bind("ID_Reward,Reward_Name,Reward_Description,Price,Picture")] Rewards rewards, IFormFile picture)
        {
            if (id != rewards.ID_Reward)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
            try
            {
                if (picture == null || picture.Length == 0)
                {
                    ModelState.AddModelError("PictureFile", "You need to reupload the image.");
                    return View(rewards);
                }
                if (rewards.Price < 1 || rewards.Price > 75000)
                {
                    ViewData["PriceE"] = "The price should be between 1 and 75000";
                    return View(rewards);
                }
                if (picture != null && picture.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await picture.CopyToAsync(memoryStream);
                        rewards.Picture = memoryStream.ToArray();
                    }
                }
                _context.Update(rewards);
                await _context.SaveChangesAsync();
                TempData["RenM"] = true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RewardsExists(rewards.ID_Reward))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("Recompensas", "Main");
            //}
            //return View(rewards);
        }
        [Authorize(Roles = "admin")]
        public IActionResult Eliminar_Recompensa(int id)
        {

            var rewards = _context.Rewards.FirstOrDefault(r => r.ID_Reward == id);
            try
            {
                // Elimina el recompensa de la base de datos
                _context.Rewards.Remove(rewards);
                _context.SaveChanges();
                TempData["RenEliminada"] = true;

                // Redirige a la acción principal
                return RedirectToAction("Recompensas", "Main");
            }catch (Exception ex)
            {
                Console.WriteLine("Constraint problem when trying to delete this reward, error: " + ex);
                using (SqlConnection connection = new SqlConnection(_varConnStr))
                {
                    connection.Open();
                    using (SqlCommand command_update = new SqlCommand("ChangeRewardState", connection))
                    {
                        command_update.CommandType = CommandType.StoredProcedure;
                        command_update.Parameters.AddWithValue("@ID_Reward", rewards.ID_Reward);
                        command_update.Parameters.AddWithValue("@newValue", 0);
                        command_update.ExecuteNonQuery();
                        connection.Close();
                        TempData["msgRewardConstraint"] = "True";
                        return RedirectToAction("Recompensas", "Main");
                    }
                }
            }
        }
        [Authorize(Roles = "admin")]
        public IActionResult Frases(bool result, Engineers model)
        {
            if (result == false)
            {
                result = validateAccountEnabled(result);
                if (result == true)
                {
                    LoadPoints(model);
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
        public IActionResult Agregar_frase(bool result, Engineers model)
        {
            if (result == false)
            {
                result = validateAccountEnabled(result);
                if (result == true)
                {
                    LoadPoints(model);
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
        public IActionResult Editar_frase(bool result, Engineers model)
        {
            if (result == false)
            {
                result = validateAccountEnabled(result);
                if (result == true)
                {
                    LoadPoints(model);
                    return View();
                }
                else
                {
                    return RedirectToAction("Login", "Auth");
                }
            }
            return RedirectToAction("Login", "Auth");
        }
        public async Task<IActionResult> Mis_ReconocimientosAsync(bool result, Engineers model)
        {
            if (result == false)
            {
                LoadPoints(model);
                LoadIdEngineer(model);
                result = validateAccountEnabled(result);
                if (result == true)
                {
                    var currentUser = await _userManager.GetUserAsync(User);
                    var engineers = await _context.Engineers
                        .Include(e => e.ApplicationUser)
                        .Include(e => e.Manager)
                        .Include(e => e.Positions)
                        .FirstOrDefaultAsync(m => m.ID_Account == currentUser.Id);

                    if (engineers != null)
                    {
                        var engineerId = engineers.ID_Engineer; // Obtén el ID del ingeniero logueado
                        ViewData["Login"] = engineerId;
                        var recognitions = await _context.Recognitions.ToListAsync();
                        foreach (var recognition in recognitions)
                        {
                            var petitionerEngineer = await _context.Engineers.FindAsync(recognition.Petitioner_Eng);
                            var evaluatorEngineer = await _context.Engineers.FindAsync(recognition.Evaluator_Admin);
                            var recognizedEngineer = await _context.Engineers.FindAsync(recognition.Recognized_Eng);

                            ViewData[$"PetitionerEngineerName_{recognition.ID_Recognition}"] = petitionerEngineer?.Name_Engineer;
                            ViewData[$"EvaluatorEngineerName_{recognition.ID_Recognition}"] = evaluatorEngineer?.Name_Engineer;
                            ViewData[$"RecognizedEngineerName_{recognition.ID_Recognition}"] = recognizedEngineer?.Name_Engineer;
                        }

                        return View(recognitions);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
        [Authorize]
        public IActionResult Perfil(bool result, Engineers model)
        {
            if (result == false)
            {
                result = validateAccountEnabled(result);
                if (result == true)
                {
                    LoadPoints(model);
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
        public IActionResult Reconocer(bool result, Engineers model)
        {
            if (result == false)
            {
                result = validateAccountEnabled(result);
                if (result == true)
                {
                    LoadPoints(model);
                    LoadIdEngineer(model);
                    var engineers = _context.Engineers
                    .Select(e => new
                    {
                        ID_Engineer = e.ID_Engineer,
                        FullName = e.Name_Engineer + " " + e.LastName_Engineer + " (" + e.ApplicationUser.Email + ")"
                    })
                    .ToList();

                    ViewData["ID_EngineerToRecognize"] = new SelectList(engineers, "ID_Engineer", "FullName");

                    return View();
                }
                else
                {
                    return RedirectToAction("Login", "Auth");
                }
            }
            return RedirectToAction("Login", "Auth");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reconocer(bool result, Engineers engineers, Recognitions recognitions, SubmitRecognitionViewModel srvw)
        {
            var status = new Status();
            var login = new LoginModel();
            var manager = new Manager();
            DateTime fechaActual = DateTime.Now;
            if (string.IsNullOrWhiteSpace(recognitions.Case_Number))
            {
                status.Message = "Case number is required";
                TempData["msgCaseNumber"] = status.Message;
                ViewBag.GetCaseNumber = recognitions.Case_Number;
                ViewBag.GetComment = recognitions.Comment;
                //ModelState.SetModelValue("Case_Number", new ValueProviderResult(srvw.Recognitions.Case_Number, CultureInfo.InvariantCulture));
                //ModelState.SetModelValue("Comment", new ValueProviderResult(srvw.Recognitions.Comment, CultureInfo.InvariantCulture));
                return RedirectToAction("Reconocer", "Main");
            }
            if (string.IsNullOrWhiteSpace(recognitions.Comment))
            {
                status.Message = "Comments are required";
                TempData["msgComment"] = status.Message;
                ViewBag.GetCaseNumber = recognitions.Case_Number;
                ViewBag.GetComment = recognitions.Comment;
                //ModelState.SetModelValue("Case_Number", new ValueProviderResult(srvw.Recognitions.Case_Number, CultureInfo.InvariantCulture));
                //ModelState.SetModelValue("Comment", new ValueProviderResult(srvw.Recognitions.Comment, CultureInfo.InvariantCulture));
                return RedirectToAction("Reconocer", "Main");
            }
            if (recognitions.Comment.Length > 500)
            {
                status.Message = "The comment should not be longer than 500 characters";
                TempData["msgRecognized"] = status.Message;
                ViewBag.GetCaseNumber = recognitions.Case_Number;
                ViewBag.GetComment = recognitions.Comment;
                //ModelState.SetModelValue("Case_Number", new ValueProviderResult(srvw.Recognitions.Case_Number, CultureInfo.InvariantCulture));
                //ModelState.SetModelValue("Comment", new ValueProviderResult(srvw.Recognitions.Comment, CultureInfo.InvariantCulture));
                return RedirectToAction("Reconocer", "Main"); 
            }
            else
            {
                recognitions.Recognition_Date = fechaActual;
                _context.Add(recognitions);
                await _context.SaveChangesAsync();
                TempData["ReconoSuccess"] = true;
                var resultNotification = await _service.SendNewRecognition(login, engineers, recognitions, manager);
                if (resultNotification.StatusCode == 1)
                {
                    status.StatusCode = 1; 
                    return RedirectToAction("Reconocimientos", "Main");
                }
                else
                {
                    status.Message = "We were unable to send the recognition";
                    TempData["msg"] = resultNotification.Message;
                }
            }
            return RedirectToAction("Index", "Main");
        }
        [Authorize]
        public IActionResult Privacy(bool result, Engineers model)
        {
            if (result == false)
            {
                result = validateAccountEnabled(result);
                if (result == true)
                {
                    LoadPoints(model);
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
        [Authorize]
        public IActionResult Metricas(bool result, Engineers model)
        {
            if (result == false)
            {
                result = validateAccountEnabled(result);
                if (result == true)
                {
                    LoadPoints(model);
                    return View();
                }
                else
                {
                    return RedirectToAction("Login", "Auth");
                }
            }
            return RedirectToAction("Login", "Auth");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Canjear_Recompensa(Purchases purchases, Engineers engineers)
        {
            var status = new Status();
            var login = new LoginModel();
            var reward = new Rewards();
            var manager = new Manager();
            LoadPoints(engineers);
            int newPoints = engineers.Points - purchases.Reward_Price;
            _context.Add(purchases);
            var result = await _context.SaveChangesAsync();
            if (result == 1)
            {
                using (SqlConnection connection = new SqlConnection(_varConnStr))
                {
                    try
                    {
                        connection.Open();
                        using (SqlCommand command_update = new SqlCommand("UpdatePoints", connection))
                        {
                            command_update.CommandType = CommandType.StoredProcedure;
                            command_update.Parameters.AddWithValue("@Engineer_ID", purchases.Engineer_ID);
                            command_update.Parameters.AddWithValue("@newPoints", newPoints);
                            command_update.ExecuteNonQuery();
                            connection.Close();
                            var resultNotification = await _service.SendNewPurchaseEmail(login, manager, engineers, reward, purchases);
                            if (resultNotification.StatusCode == 1)
                            {
                                status.StatusCode = 1;
                                status.Message = "Your purchase has been successful";
                                TempData["RewardBuy"] = true;
                                return RedirectToAction("Index", "Main");
                            }
                            else
                            {
                                TempData["msg"] = resultNotification.Message;
                                return RedirectToAction("Index", "Main");
                            }        
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
                status.Message = "The purchase failed";
                TempData["msg"] = status.Message;
                return RedirectToAction("Index", "Main");
            }
        }
        private bool RewardsExists(int id)
        {
            return (_context.Rewards?.Any(e => e.ID_Reward == id)).GetValueOrDefault();
        }
    }
}