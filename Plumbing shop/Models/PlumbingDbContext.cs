using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Plumbing_shop.Models
{
    public class PlumbingDbContext : DbContext
    {
        public DbSet<Entity> Entities { get; set; }
        protected readonly IConfiguration Configuration;
        public PlumbingDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // важно
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Контекст получает строку подключения.
            optionsBuilder.UseSqlServer(Constants.connectMSSQL);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entity>().Property(e => e.Id).UseIdentityColumn();
        }
        
    }
}
