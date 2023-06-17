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
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;
using Newtonsoft.Json;

namespace sistema_reconocimiento.Controllers
{
    public class MainController : Controller
    {
        private readonly INotificationEmailService _service;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<MainController> _logger;
        private readonly IConfiguration _configuration;
        private readonly string _varConnStr;
        public MainController(INotificationEmailService service, ILogger<MainController> logger, IConfiguration configuration, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this._service = service;
            _context = context;
            _logger = logger;
            _userManager = userManager;
            _configuration = configuration;
            _varConnStr = _configuration.GetValue<string>("ConnectionStrings:DBConnectionString");
            _userManager = userManager;
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
            List<Rewards> rewards = await _context.Rewards.ToListAsync();

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
                    ViewData["ID_Account"] = new SelectList(_context.ApplicationUser, "Id", "Id");
                    ViewData["ID_Manager"] = new SelectList(_context.Set<Manager>(), "ID_Manager", "LastName_Manager");
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Agregar_ingeniero([Bind("ID_Engineer,Name_Engineer,LastName_Engineer,Position,Points,ID_Account,ID_Manager")] Engineers engineers)
        {
            
            _context.Add(engineers);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            ViewData["ID_Account"] = new SelectList(_context.ApplicationUser, "Id", "Id", engineers.ID_Account);
            ViewData["ID_Manager"] = new SelectList(_context.Set<Manager>(), "ID_Manager", "LastName_Manager", engineers.ID_Manager);
            ViewData["Position"] = new SelectList(_context.Positions, "ID_Position", "Position_Name", engineers.Position);
            return View(engineers);
        }

        [Authorize(Roles = "admin")]
        public IActionResult Editar_ingeniero(bool result, Engineers model)
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
                    int pageSize = 10; // Número de elementos por página
                    int pageNumber = page ?? 1; // Número de página actual
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
            return RedirectToAction(nameof(Reconocimientos));
        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Recompensas(bool result, Engineers model)
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
        public async Task<IActionResult> Agregar_recompensa(Rewards rewards)
        {
            var status = new Status();
            if (rewards.Reward_Name != null && rewards.Reward_Description != null && rewards.Price != null && rewards.PictureFile != null)
            {
                if (rewards.PictureFile != null && rewards.PictureFile.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        rewards.PictureFile.CopyTo(memoryStream);
                        rewards.Picture = memoryStream.ToArray();
                    }
                    _context.Add(rewards);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Recompensas");
                }
                else
                {
                    status.Message = "You need to upload an image";
                    TempData["msg"] = status.Message;
                    ModelState.SetModelValue("Reward_Name", new ValueProviderResult(rewards.Reward_Name, CultureInfo.InvariantCulture));
                    ModelState.SetModelValue("Reward_Description", new ValueProviderResult(rewards.Reward_Description, CultureInfo.InvariantCulture));
                    return View();
                }
            }
            else
            {
                status.Message = "Please fill all the necessary information";
                TempData["msg"] = status.Message;
                ModelState.SetModelValue("Reward_Name", new ValueProviderResult(rewards.Reward_Name, CultureInfo.InvariantCulture));
                ModelState.SetModelValue("Reward_Description", new ValueProviderResult(rewards.Reward_Description, CultureInfo.InvariantCulture));
                //ModelState.SetModelValue("Price", new ValueProviderResult(rewards.Price, CultureInfo.InvariantCulture)); -- tira error porque el tipo de dato es int, si se pasa a string deja de dar error
                return View();
            }
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
        public IActionResult Editar_recompensa(bool result, Engineers model)
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
        [Authorize]
        public IActionResult Mis_Reconocimientos(bool result, Engineers model)
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
                        FullName = e.Name_Engineer + " " + e.LastName_Engineer
                    })
                    .ToList();

                    ViewData["ID_EngineerToRecognize"] = new SelectList(engineers, "ID_Engineer", "FullName");

                   // ViewData["ID_EngineerToRecognize"] = new SelectList(_context.Engineers, "ID_Engineer", "Name_Engineer");
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
        public async Task<IActionResult> Agregar_Reconocimiento(bool result, Engineers engineers, Recognitions recognitions)
        {
            var status = new Status();
            var login = new LoginModel();
            var manager = new Manager();
            _context.Add(recognitions);
            await _context.SaveChangesAsync();
            var resultNotification = await _service.SendNewRecognition(login, engineers, recognitions, manager);
            if (resultNotification.StatusCode == 1)
            {
                status.StatusCode = 1;
                return RedirectToAction("Mis_Reconocimientos", "Main");
            }
            else
            {
                TempData["msg"] = resultNotification.Message;
                return RedirectToAction("Index", "Main");
            }
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
                                TempData["msg"] = status.Message;
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
    }
}