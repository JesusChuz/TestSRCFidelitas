using System.ComponentModel.DataAnnotations;
namespace sistema_reconocimiento.Models
{
    public class Manager
    {
        [Key]
        [Required]
        public int ID_Manager { get; set; }
        [Required]
        public string Name_Manager { get; set; }
        [Required]
        public string LastName_Manager { get; set; }

        public string Email { get; set; }

        public virtual ICollection<Engineers> Engineers { get; set; }
    }
}
