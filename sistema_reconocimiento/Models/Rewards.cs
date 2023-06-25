using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sistema_reconocimiento.Models
{
    public class Rewards
    {
        [Key]
        [Required]
        public int ID_Reward { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "The name must be between 2 and 50 characters.")]
        public string Reward_Name { get; set; }
        [Required(ErrorMessage = "Description requred")]
        [StringLength(150, MinimumLength = 5, ErrorMessage = "Description must be between 5 and 150 characters.")]
        public string Reward_Description { get; set; }
        [Required(ErrorMessage = "Reward's price is required")]
        [Range(1, 75000, ErrorMessage = "Price must be between 1 and 75000")]
        public int Price { get; set; }
        [Required]
        public byte[]? Picture { get; set; }
        [NotMapped]
        public IFormFile PictureFile { get; set; }

        public virtual ICollection<Purchases> Purchases { get; set; }
        [NotMapped]
        public string Base64Image { get; set; }
    }
}
