using Microsoft.EntityFrameworkCore;
using PresMed.Models;

namespace PresMed.Data {
    public class BancoContext : DbContext {

        public BancoContext(DbContextOptions<BancoContext> options) : base(options) { }

        public DbSet<Doctor> Doctor { get; set; }

    }
}
