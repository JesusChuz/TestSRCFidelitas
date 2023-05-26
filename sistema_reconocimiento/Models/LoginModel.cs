using System.ComponentModel.DataAnnotations;
//Clase para proceso de creacion de cuenta con Identity, el cual necesita de la existencia de las tablas AspNetUsers, AspNetUserRoles, AspNetRoles. etc...
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
    }
}
