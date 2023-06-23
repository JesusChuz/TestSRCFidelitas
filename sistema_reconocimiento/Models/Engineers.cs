using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sistema_reconocimiento.Models
{
    public class Engineers
    {
        [Key]
        [Required]
        public int ID_Engineer { get; set; }
<<<<<<< HEAD
=======
      
>>>>>>> 996bfd33ae1f3c389c05516131f52ce72c62ba76
        public string Name_Engineer { get; set; }
   
        public string LastName_Engineer { get; set; }
        //public int Position_ID { get; set; } //charge in the company: manager, TL, SME, etc...
<<<<<<< HEAD

        public int Points { get; set; }

        public int Position { get; set; }
        [ForeignKey("Position")]
        public virtual Positions Positions { get; set; }
        //public byte[] Picture { get; set; }

        public String ID_Account { get; set; }
        [ForeignKey("ID_Account")]
        public virtual ApplicationUser ApplicationUser { get; set; }
=======
      
        public int Position { get; set; }
       
        public int Points { get; set; }
        [ForeignKey("Position")]
        public virtual Positions Positions { get; set; }
        //public byte[] Picture { get; set; }
        public String ID_Account { get; set; }
        [ForeignKey("ID_Account")]
        public virtual ApplicationUser ApplicationUser { get; set; }

>>>>>>> 996bfd33ae1f3c389c05516131f52ce72c62ba76
        //public virtual AccountRegistration AccountRegistration { get; set; }

        public int ID_Manager { get; set; }
        [ForeignKey("ID_Manager")]
        public virtual Manager Manager { get; set; }
<<<<<<< HEAD
        [NotMapped]
        public virtual ICollection<Recognitions> PetitionerRecognitions { get; set; }
        [NotMapped]
        public virtual ICollection<Recognitions> RecognizedRecognitions { get; set; }
        [NotMapped]
        public virtual ICollection<Recognitions> Evaluator_Admin { get; set; }

        [NotMapped]
        public string Email { get; set; }
=======

     





>>>>>>> 996bfd33ae1f3c389c05516131f52ce72c62ba76

    }
}
