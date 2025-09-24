using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HDA.Domain.Entities;
using HDA.Domain.Identity;

namespace HDA.Domain
{
    public class MyDbContext : IdentityDbContext<IdentityUser>
    {

        public DbSet<Joueur> Joueurs { get; set; }
        public DbSet<JoueursMatch> JoueursMatchs { get; set; }
        public DbSet<MatchSoccer> MatchsSoccer { get; set; }
        public DbSet<FormuleRepas> FormulesRepas { get; set; }

        public DbSet<MembreZero> MembresZero { get; set; }
        public DbSet<Membre> Membres { get; set; }
        public DbSet<ResasZero> ResasZeros { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Joueur>().HasIndex(j => j.Mail).IsUnique();

            //Create and seed Roles
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id =  IdentityRoles.AdminId, Name = IdentityRoles.AdminName, NormalizedName = IdentityRoles.AdminName.ToUpper() }
                );

            //Create and seed Admin User
            var adminUserId = Guid.NewGuid().ToString();

            modelBuilder.Entity<IdentityUser>().HasData(
                new IdentityUser
                {
                    Id = adminUserId,
                    UserName = "admin",
                    NormalizedUserName = "admin".ToUpper(),
                    PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "secretPassword!01")
                });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { RoleId = IdentityRoles.AdminId, UserId = adminUserId }
                );
        }

    }
}