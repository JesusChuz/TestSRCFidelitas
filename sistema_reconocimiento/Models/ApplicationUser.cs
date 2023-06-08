using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace sistema_reconocimiento.Models
{
    public class ApplicationUser:IdentityUser
    {
        
        public string Name { get; set; }
        public bool IsNew { get; set; }

        public virtual ICollection<Engineers> Engineers { get; set; }
    }
}
