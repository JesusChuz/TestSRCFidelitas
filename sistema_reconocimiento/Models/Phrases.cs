using System.ComponentModel.DataAnnotations;
namespace sistema_reconocimiento.Models
{
    public class Phrases
    {
        [Key]
        [Required]
        public int Phrases_ID { get; set; }

        [Required]
        public string Phrase { get; set; }
        [Required]
        public int Engineer_ID { get; set; }
    }
}
