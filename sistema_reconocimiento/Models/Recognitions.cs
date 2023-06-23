using System.ComponentModel.DataAnnotations;
<<<<<<< HEAD
using System.ComponentModel.DataAnnotations.Schema;

=======
>>>>>>> 996bfd33ae1f3c389c05516131f52ce72c62ba76
namespace sistema_reconocimiento.Models
{
    public class Recognitions
    {
        [Key]
        [Required]
        public int ID_Recognition { get; set; }

<<<<<<< HEAD
         public int Petitioner_Eng { get; set; }
         [ForeignKey("Petitioner_Eng")]
         public virtual Engineers PetitionerEngineer { get; set; }

         public int Recognized_Eng { get; set; }
         [ForeignKey("Recognized_Eng")]
        public virtual Engineers RecognizedEngineer { get; set; }

        public string Recognition_State { get; set; }
        [Required(ErrorMessage = "Case number required")]
        public string Case_Number { get; set; }
        [Required(ErrorMessage = "Comments are requred")]
        public string Comment { get; set; }

        public int Evaluator_Admin { get; set; }
        [ForeignKey("Evaluator_Admin")]
        public virtual Engineers EvaluatorAdmin { get; set; }
        [NotMapped]
        public string Petitioner_EngEmail { get; set; }
        [NotMapped]
        public string Recognized_EngEmail { get; set; }
        [NotMapped]
        public string Petitioner_EngFullName { get; set; }
        [NotMapped]
        public int ID_EngineerRec { get; set; }
        [NotMapped]
        public int Points_EngineerRec { get; set; }

        public DateTime Recognition_Date { get; set; }

=======
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
>>>>>>> 996bfd33ae1f3c389c05516131f52ce72c62ba76
    }
}
