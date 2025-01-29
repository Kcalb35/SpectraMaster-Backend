using Microsoft.EntityFrameworkCore;

namespace SpectraMaster.Data
{
    public class ManagerDbContext:DbContext
    {
        public DbSet<Manager> Managers { get; set; }

        public ManagerDbContext(DbContextOptions<ManagerDbContext> opt):base(opt)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Manager>().HasData(
                new Manager(){Id = 1,Username = "gc",Password = "gc135"}
                );
        }
    }
}