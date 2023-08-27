using Microsoft.EntityFrameworkCore;
using PharmacyInventory_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyInventory_Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
        public DbSet<Drug> drug { get; set; }
        public DbSet<Brand> brand { get; set; }
        public DbSet<Unit> unit { get; set; }
        public DbSet<Supplier> supplier { get; set; }
        public DbSet<User> user { get; set; }
        public DbSet<GenericName> genericName { get; set; }
    }
}
