using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sistema_reconocimiento.Models
{
    public class Rewards
    {
        [Key]
        [Required]
        public int ID_Reward { get; set; }

        [Required]
        public string Reward_Name { get; set; }
        [Required]
        public string Reward_Description { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public byte[]? Picture { get; set; }
        [NotMapped]
        public IFormFile PictureFile { get; set; }

        public virtual ICollection<Purchases> Purchases { get; set; }
    }
}
