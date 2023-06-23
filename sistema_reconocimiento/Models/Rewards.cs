using System.ComponentModel.DataAnnotations;
<<<<<<< HEAD
using System.ComponentModel.DataAnnotations.Schema;
=======
>>>>>>> 996bfd33ae1f3c389c05516131f52ce72c62ba76

namespace sistema_reconocimiento.Models
{
    public class Rewards
    {
        [Key]
        [Required]
        public int ID_Reward { get; set; }
<<<<<<< HEAD
        [Required(ErrorMessage = "Name is required")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "The name must be between 5 and 30 characters.")]
        public string Reward_Name { get; set; }
        [Required(ErrorMessage = "Description requred")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Description must be between 5 and 50 characters.")]
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
=======

        [Required]
        public string Reward_Name { get; set; }
        [Required]
        public string Reward_Description { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public byte[] Picture { get; set; }
>>>>>>> 996bfd33ae1f3c389c05516131f52ce72c62ba76
    }
}
