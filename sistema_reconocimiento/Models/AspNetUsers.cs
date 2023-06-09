using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Runtime.InteropServices;

namespace sistema_reconocimiento.Models
{
    public class AspNetUsers 
    {
        public string id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public string? Role { get; set; }

        public virtual ICollection<Engineers> Engineers { get; set; }

    }
}
