using Microsoft.EntityFrameworkCore;
using Richmond.Domain.DomainObjects;
using Richmond.Domain.Entities;
using System.Linq;

namespace Richmond.Domain.Data
{
    public class RichMondContext :  DbContext
    {
        public RichMondContext(DbContextOptions<RichMondContext> options)
            : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }
        public DbSet<Account> Accounts { get; set; }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(50)");
            modelBuilder.Ignore<Email>();

            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RichMondContext).Assembly);
        }
    }
}
