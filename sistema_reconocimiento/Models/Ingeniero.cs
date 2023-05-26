using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace sistema_reconocimiento.Models
{
    public class Ingeniero: IdentityUser
    {
        [Required(ErrorMessage = "Es necesario")]
        public int ID_Ingeniero { get; set; }
        [Required(ErrorMessage = "Es necesario")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Es necesario")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Es necesario")]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "Es necesario")]
        public int Rol { get; set; }
        [Required(ErrorMessage = "Es necesario")]
        public int Posicion { get; set; }
        [Required(ErrorMessage = "Es necesario")]
        public int Puntos { get; set; }
        [Required(ErrorMessage = "Es necesario")]
        public string Password { get; set; }
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

}
