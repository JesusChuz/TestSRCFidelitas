using System.ComponentModel.DataAnnotations;

namespace sistema_reconocimiento.Models
{
    public class Positions //charge in the company: manager, TL, SME, etc...
    {
        [Key]
        [Required]
        public int ID_Position{ get; set; }

        [Required]
        public string Position_Name { get; set; }

        //public virtual Engineers engineer { get; set; }
        public virtual ICollection<Engineers> Engineers { get; set; }
    }
}
