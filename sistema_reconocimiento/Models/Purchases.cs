using System.ComponentModel.DataAnnotations;
namespace sistema_reconocimiento.Models
{
    public class Purchases
    {
        [Key]
        [Required]
        public int ID_Purchase { get; set; }

        [Required]
        public int Engineer_ID { get; set; }
        [Required]
        public int Reward_ID { get; set; }
        [Required]
        public int Reward_Price { get; set; }
    }
}
