using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sistema_reconocimiento.Models
{
    public class Recognitions
    {
        [Key]
        [Required]
        public int ID_Recognition { get; set; }

         public int Petitioner_Eng { get; set; }
         [ForeignKey("Petitioner_Eng")]
         public virtual Engineers PetitionerEngineer { get; set; }

         public int Recognized_Eng { get; set; }
         [ForeignKey("Recognized_Eng")]
        public virtual Engineers RecognizedEngineer { get; set; }

        public string Recognition_State { get; set; }
      
        public string Case_Number { get; set; }
     
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

    }
}
