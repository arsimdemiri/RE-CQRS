using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RealEstate.Models;
using RealEstate.Models.Common;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstate.Data
{
    public class RealEstateDbContext : IdentityDbContext<User, Role, Guid>
    {
        public DbSet<Property> Properties { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<PropertyFeatures> PropertyFeatures { get; set; }
        public DbSet<PropertyType> PropertyTypes { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<PropertyDocument> PropertyDocuments { get; set; }

        public RealEstateDbContext(DbContextOptions<RealEstateDbContext> options) : base(options) {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RealEstateDbContext).Assembly);
        }
        public async Task<int> SaveChangesAsync(string username = "SYSTEM")
        {
            foreach (var entry in base.ChangeTracker.Entries<BaseEntity>()
                .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
            {
                entry.Entity.UpdatedDate = DateTime.Now;
                entry.Entity.UpdatedBy = username;

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedDate = DateTime.Now;
                    entry.Entity.CreatedBy = username;
                }
            }

            var result = await base.SaveChangesAsync();

            return result;
        }
    }
}
