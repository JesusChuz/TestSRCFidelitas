using System.ComponentModel.DataAnnotations;

namespace sistema_reconocimiento.Models
{
    public class LoginModel
    {
        [Key]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        public string OldPassword { get; set; }
        [Compare("Password")]
        public string ConfirmPassword { get; set;}
    }
}
