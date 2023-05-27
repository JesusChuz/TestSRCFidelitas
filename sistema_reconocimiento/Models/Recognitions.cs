using System.ComponentModel.DataAnnotations;
namespace sistema_reconocimiento.Models
{
    public class Recognitions
    {
        [Key]
        [Required]
        public int ID_Recognition { get; set; }

        [Required]
        public int Petitioner_Eng { get; set; }
        [Required]
        public int Recognized_Eng { get; set; }
        [Required]
        public string Recognition_State { get; set; }
        [Required]
        public string Case_Number { get; set; }
        [Required]
        public string Comment { get; set; }
        [Required]
        public int Evaluator_Admin { get; set; }
    }
}
