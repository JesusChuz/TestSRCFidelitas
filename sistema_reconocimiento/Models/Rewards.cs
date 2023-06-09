using System.ComponentModel.DataAnnotations;

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
        public byte[] Picture { get; set; }
    }
}
