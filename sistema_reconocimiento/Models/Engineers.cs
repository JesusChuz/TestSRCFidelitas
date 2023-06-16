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
      
        public string Name_Engineer { get; set; }
   
        public string LastName_Engineer { get; set; }
        //public int Position_ID { get; set; } //charge in the company: manager, TL, SME, etc...
      
        public int Position { get; set; }
       
        public int Points { get; set; }
        [ForeignKey("Position")]
        public virtual Positions Positions { get; set; }
        //public byte[] Picture { get; set; }
        public String ID_Account { get; set; }
        [ForeignKey("ID_Account")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        //public virtual AccountRegistration AccountRegistration { get; set; }

        public int ID_Manager { get; set; }
        [ForeignKey("ID_Manager")]
        public virtual Manager Manager { get; set; }

<<<<<<< HEAD
=======
     






>>>>>>> 5214b57e3f10b832105456c72dacff5b1de60d2b
    }
}
