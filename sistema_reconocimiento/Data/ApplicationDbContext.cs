using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using sistema_reconocimiento.Models;

namespace sistema_reconocimiento.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<sistema_reconocimiento.Models.Ingeniero>? Ingeniero { get; set; }
        public DbSet<sistema_reconocimiento.Models.LoginModel>? LoginModel { get; set; }
    }
}
