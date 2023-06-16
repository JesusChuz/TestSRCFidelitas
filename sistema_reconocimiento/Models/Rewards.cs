using System.ComponentModel.DataAnnotations;
<<<<<<< HEAD
using System.ComponentModel.DataAnnotations.Schema;
=======
>>>>>>> 5214b57e3f10b832105456c72dacff5b1de60d2b

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
<<<<<<< HEAD
        public byte[]? Picture { get; set; }
        [NotMapped]
        public IFormFile PictureFile { get; set; }

        public virtual ICollection<Purchases> Purchases { get; set; }
=======
        public byte[] Picture { get; set; }
>>>>>>> 5214b57e3f10b832105456c72dacff5b1de60d2b
    }
}
