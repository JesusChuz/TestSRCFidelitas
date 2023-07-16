using System.ComponentModel.DataAnnotations.Schema;

namespace sistema_reconocimiento.Models
{
    public class Top5
    {
        [NotMapped]
        public int RecognizedEng { get; set; }
        [NotMapped]
        public int Count { get; set; }
        [NotMapped]
        public string Email { get; set; }
        [NotMapped]
        public string FullName { get; set; }
        [NotMapped]
        public string PositionName { get; set; }
        //[NotMapped]
        //public string Picture { get; set; }
    }

}
