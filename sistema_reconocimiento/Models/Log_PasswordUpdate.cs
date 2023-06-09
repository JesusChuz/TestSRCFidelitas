using System.ComponentModel.DataAnnotations;
namespace sistema_reconocimiento.Models
{
    public class Log_PasswordUpdate
    {
        [Key]
        [Required]
        public int ID_Change { get; set; }

        [Required]
        public string Reason { get; set; }
        [Required]
        public string ID_Engineer { get; set; }
        [Required]
        public DateTime Change_Date { get; set; }
    }
}
