using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace sistema_reconocimiento.Models
{
    public class AccountRegistration
    {
        [Required]
        [Key]
        public string id { get; set; }
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
        [Required] 
        public string? Role { get; set; }
<<<<<<< HEAD
        public bool IsNew { get; set; }
=======
>>>>>>> 996bfd33ae1f3c389c05516131f52ce72c62ba76

        //public virtual ICollection<Engineers> Engineers { get; set; }


    }
}
