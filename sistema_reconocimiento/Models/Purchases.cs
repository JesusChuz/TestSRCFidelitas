using System.ComponentModel.DataAnnotations;
<<<<<<< HEAD
using System.ComponentModel.DataAnnotations.Schema;

=======
>>>>>>> 5214b57e3f10b832105456c72dacff5b1de60d2b
namespace sistema_reconocimiento.Models
{
    public class Purchases
    {
        [Key]
        [Required]
        public int ID_Purchase { get; set; }

        [Required]
        public int Engineer_ID { get; set; }
<<<<<<< HEAD

        [Required]
        public int Reward_Price { get; set; }

        public int Reward_ID { get; set; }
        [ForeignKey("Reward_ID")]
        public virtual Rewards Rewards { get; set; }
        [NotMapped]
        public int Points { get; set; }
=======
        [Required]
        public int Reward_ID { get; set; }
        [Required]
        public int Reward_Price { get; set; }
>>>>>>> 5214b57e3f10b832105456c72dacff5b1de60d2b
    }
}
