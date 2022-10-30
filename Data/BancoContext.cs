using Microsoft.EntityFrameworkCore;
using PresMed.Models;

namespace PresMed.Data {
    public class BancoContext : DbContext {

        public BancoContext(DbContextOptions<BancoContext> options) : base(options) { }

        public DbSet<Person> Person { get; set; }
        public DbSet<Procedures> Procedure { get; set; }

        public DbSet<Time> Time { get; set; }
        public DbSet<Scheduling> Scheduling { get; set; }
        public DbSet<ClinicOpening> ClinicOpening { get; set; }
        public DbSet<Medicine> Medicine { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Person>()
                .HasIndex(p => p.User)
                .IsUnique(true);
            modelBuilder.Entity<Person>()
                .HasIndex(p => p.Cpf)
                .IsUnique(true);
            modelBuilder.Entity<Procedures>()
                 .HasIndex(p => p.Name)
                 .IsUnique(true);
            modelBuilder.Entity<Procedures>()
                .HasIndex(p => p.Tuss)
                .IsUnique(true);
            modelBuilder.Entity<Person>(etb => {
                etb.Property(t => t.Crm)
                   .IsRequired(false);
                etb.HasIndex(t => t.Crm)
                .IsUnique(true);
                etb.Property(t => t.Speciality)
                .IsRequired(false);
            });

        }

    }
}
