using System.ComponentModel.DataAnnotations;
namespace sistema_reconocimiento.Models
{
    public class Phrases
    {
        [Key]
        [Required]
        public int Phrases_ID { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "The phrase must be between 8 and 100 characters.")]
        public string Phrase { get; set; }
        [Required]
        public Boolean States { get; set; }
    }
}
