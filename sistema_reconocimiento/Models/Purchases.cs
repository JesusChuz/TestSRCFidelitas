using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public int Reward_Price { get; set; }

        public int Reward_ID { get; set; }
        [ForeignKey("Reward_ID")]
        public virtual Rewards Rewards { get; set; }
        [NotMapped]
        public int Points { get; set; }
    }
}
