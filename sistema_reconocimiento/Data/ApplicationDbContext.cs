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
        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            //modelBuilder.Entity<IdentityUser>().ToTable("Engineers");

            modelBuilder.Entity<IdentityUser>().Ignore(c => c.NormalizedUserName)
                                   .Ignore(c => c.NormalizedEmail)
                                   .Ignore(c => c.EmailConfirmed)
                                   .Ignore(c => c.SecurityStamp)
                                   .Ignore(c => c.ConcurrencyStamp)
                                   .Ignore(c => c.PhoneNumber)
                                   .Ignore(c => c.PhoneNumberConfirmed)
                                   .Ignore(c => c.TwoFactorEnabled)
                                   .Ignore(c => c.LockoutEnd)
                                   .Ignore(c => c.AccessFailedCount);

            modelBuilder.Entity<IdentityRole>().Ignore(c => c.NormalizedName)
                                    .Ignore(c => c.ConcurrencyStamp);


            /*modelBuilder.Entity<Positions>()
                        .HasMany(u => u.engineer)
                        .WithOne(a => a.position)
                        .HasForeignKey(a => a.Position_ID);*/
        /* modelBuilder.Entity<Positions>()
         .HasMany(u => u.engineer)
         .WithOne(a => a.position)
         .HasForeignKey(a => a.Position_ID);*/
    //}
    //public DbSet<sistema_reconocimiento.Models.Ingeniero? Ingeniero { get; set; } 
    public DbSet<sistema_reconocimiento.Models.LoginModel>? LoginModel { get; set; }
    
        //public DbSet<sistema_reconocimiento.Models.Engineers>? Engineers { get; set; }
        public DbSet<sistema_reconocimiento.Models.CSAT>? CSAT { get; set; }
        public DbSet<sistema_reconocimiento.Models.Log_PasswordUpdate>? Log_PasswordUpdate { get; set; }
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
