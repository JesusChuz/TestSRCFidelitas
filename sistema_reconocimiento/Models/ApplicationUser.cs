using Microsoft.AspNetCore.Identity;

namespace sistema_reconocimiento.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }

    }
}
