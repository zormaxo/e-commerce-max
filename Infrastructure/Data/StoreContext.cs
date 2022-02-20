using System.Reflection;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
  public class StoreContext : DbContext
  {
    public StoreContext(DbContextOptions<StoreContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<ProductBrand> ProductBrands { get; set; }
    public DbSet<ProductType> ProductTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      modelBuilder.Entity<BaseAuditableEntity>().HasQueryFilter(m => EF.Property<bool>(m, "IsDeleted") == false);
      modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {

      var entries = ChangeTracker
        .Entries()
        .Where(e => e.Entity is BaseAuditableEntity &&
              (e.State == EntityState.Added || e.State == EntityState.Modified));

      foreach (var entityEntry in entries)
      {
        ((BaseAuditableEntity)entityEntry.Entity).UpdateDate = DateTime.Now;

        // if (entityEntry.State == EntityState.Added)
        // {
        //   ((BaseAuditableEntity)entityEntry.Entity).CreationDate = DateTime.Now;
        // }
      }
      return base.SaveChangesAsync();
    }
  }
}