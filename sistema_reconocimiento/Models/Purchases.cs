using System.ComponentModel.DataAnnotations;
<<<<<<< HEAD
using System.ComponentModel.DataAnnotations.Schema;

=======
>>>>>>> 996bfd33ae1f3c389c05516131f52ce72c62ba76
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
>>>>>>> 996bfd33ae1f3c389c05516131f52ce72c62ba76
    }
}
