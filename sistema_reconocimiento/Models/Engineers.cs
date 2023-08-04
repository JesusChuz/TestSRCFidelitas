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
        [Required]
        [StringLength(30, ErrorMessage = "The name should not be longer than 30 characters")]
        public string Name_Engineer { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "The last name should not be longer than 30 characters")]
        public string LastName_Engineer { get; set; }
        //public int Position_ID { get; set; } //charge in the company: manager, TL, SME, etc...

        public int Points { get; set; }

        [Required(ErrorMessage = "Select a position")]
        public int Position { get; set; }
        [ForeignKey("Position")]
        public virtual Positions Positions { get; set; }

        public byte[]? Picture { get; set; }
        [NotMapped]
        public IFormFile PictureFile { get; set; }
        [NotMapped]
        public string Base64Image { get; set; }

        public String ID_Account { get; set; }
        [ForeignKey("ID_Account")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        //public virtual AccountRegistration AccountRegistration { get; set; }

        [Required(ErrorMessage = "Select a manager")]
        public int ID_Manager { get; set; }
        [ForeignKey("ID_Manager")]
        public virtual Manager Manager { get; set; }

        [NotMapped]
        public virtual ICollection<Recognitions> PetitionerRecognitions { get; set; }
        [NotMapped]
        public virtual ICollection<Recognitions> RecognizedRecognitions { get; set; }
        [NotMapped]
        public virtual ICollection<Recognitions> Evaluator_Admin { get; set; }

        [NotMapped]
        public string Email { get; set; }

    }
}
