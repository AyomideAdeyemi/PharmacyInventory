using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PharmacyInventory_Domain.Entities;
using PharmacyInventory_Infrastructure.Repository;

namespace PharmacyInventory_Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          modelBuilder.ApplyConfiguration(new RoleConfigurations());

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Drug> drug { get; set; }
        public DbSet<Brand> brand { get; set; }
        public DbSet<Unit> unit { get; set; }
        public DbSet<Supplier> supplier { get; set; }
        public DbSet<GenericName> genericName { get; set; }
    }
}
