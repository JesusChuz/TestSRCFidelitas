﻿using System.ComponentModel.DataAnnotations;

namespace sistema_reconocimiento.Models
{
    public class AccountRegistrationModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#$^+=!*()@%&]).{6,}$", ErrorMessage = "Minum length 6 and must contain 1 uppercase, 1 lowercase, 1 special caracter and 1 digit")]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string PasswordConfirm { get; set; }

        [Required] public string? Role { get; set; }
    }
}
