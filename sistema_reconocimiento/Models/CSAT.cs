using System.ComponentModel.DataAnnotations;
namespace sistema_reconocimiento.Models
{
    public class CSAT
    {
        [Key]
        [Required]
        public int ID_Survey { get; set; }

        [Required]
        public int Score { get; set; }
        [Required]
        public string Comments { get; set; }
        [Required]
        public string Email_Engineer { get; set; }
        [Required]
        public int DTC { get; set; }
        [Required]
        public int Engineer_ID { get; set; }
    }
}
