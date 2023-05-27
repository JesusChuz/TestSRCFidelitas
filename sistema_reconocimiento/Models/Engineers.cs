using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace sistema_reconocimiento.Models
{
    public class Engineers
    {
        [Key]
        [Required]
        public int ID_Engineer { get; set; }
        [Required]
        public string Name_Engineer { get; set; }
        [Required]
        public string LastName_Engineer { get; set; }
        //public int Position_ID { get; set; } //charge in the company: manager, TL, SME, etc...
        [Required]
        public int Position { get; set; }
        [Required]
        public int Points { get; set; }
        public byte[] Picture { get; set; }
        public int ID_Account { get; set; }
        /*[Required]
        public int Position_ID { get; set; }
        //public virtual ICollection<Positions> Position_ID { get; set; }
        public virtual Positions position { get; set; }*/

    }
}
