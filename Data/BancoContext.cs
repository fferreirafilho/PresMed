using Microsoft.EntityFrameworkCore;
using PresMed.Models;

namespace PresMed.Data {
    public class BancoContext : DbContext {

        public BancoContext(DbContextOptions<BancoContext> options) : base(options) { }

        public DbSet<Person> Person { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Person>()
                .HasIndex(p => p.User)
                .IsUnique(true);
            modelBuilder.Entity<Person>()
                .HasIndex(p => p.Cpf)
                .IsUnique(true);
            modelBuilder.Entity<Person>()
                .HasIndex(p => p.Crm)
                .IsUnique(true);
        }

    }
}
