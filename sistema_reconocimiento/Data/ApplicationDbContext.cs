using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using sistema_reconocimiento.Models;
using System.ComponentModel.DataAnnotations;

namespace sistema_reconocimiento.Data
{
    //public class ApplicationDbContext : IdentityDbContext<IdentityUser>
     public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
     {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<sistema_reconocimiento.Models.Engineers>? Engineers { get; set; }
        public DbSet<sistema_reconocimiento.Models.LoginModel>? LoginModel { get; set; }
    
        //public DbSet<sistema_reconocimiento.Models.Engineers>? Engineers { get; set; }
        public DbSet<sistema_reconocimiento.Models.CSAT>? CSAT { get; set; }
        public DbSet<sistema_reconocimiento.Models.Log_PasswordUpdate>? Log_PasswordUpdate { get; set; }
        public DbSet<sistema_reconocimiento.Models.Manager>? Manager { get; set; }
        public DbSet<sistema_reconocimiento.Models.Phrases>? Phrases { get; set; }
        public DbSet<sistema_reconocimiento.Models.Positions>? Positions { get; set; }
        public DbSet<sistema_reconocimiento.Models.Purchases>? Purchases { get; set; }
        public DbSet<sistema_reconocimiento.Models.Recognitions>? Recognitions { get; set; }
        public DbSet<sistema_reconocimiento.Models.Rewards>? Rewards { get; set; }
        public DbSet<sistema_reconocimiento.Models.ApplicationUser>? ApplicationUser { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });
          

            base.OnModelCreating(modelBuilder);


        }
       
    }
}
