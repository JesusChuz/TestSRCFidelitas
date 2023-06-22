using Microsoft.AspNetCore.Http;

namespace sistema_reconocimiento.Models
{
    public class FileUpload
    {
        public IFormFile file { get; set; } 
        public string reward { get; set; }  
    }
}
