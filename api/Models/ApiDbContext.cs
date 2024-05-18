using Microsoft.EntityFrameworkCore;
using API.Models;

namespace API.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Chambre> Chambres { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Commande> Commandes { get; set; }
        public DbSet<Condiment> Condiments { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Facturation> Facturations { get; set; }
        public DbSet<Menage> Menages { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Plat> Plats { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<jourTravail> JourTravail { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("user");
            modelBuilder.Entity<Chambre>().ToTable("chambre");
            modelBuilder.Entity<Reservation>().ToTable("reservation");
            modelBuilder.Entity<Commande>().ToTable("commande");
            modelBuilder.Entity<Condiment>().ToTable("condiment");
            modelBuilder.Entity<Contact>().ToTable("contact");
            modelBuilder.Entity<Facturation>().ToTable("facturation");
            modelBuilder.Entity<Menage>().ToTable("menage");
            modelBuilder.Entity<Menu>().ToTable("menu");
            modelBuilder.Entity<Plat>().ToTable("plat");
            modelBuilder.Entity<Service>().ToTable("service");
            modelBuilder.Entity<jourTravail>().ToTable("jourTravail");
        }
    }
}