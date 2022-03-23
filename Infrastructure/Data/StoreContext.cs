using System.Linq.Expressions;
using System.Reflection;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Infrastructure.Data
{
  public class StoreContext : DbContext
  {
    public StoreContext(DbContextOptions<StoreContext> options) : base(options)
    {
    }

    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductBrand> ProductBrands { get; set; }
    public DbSet<ProductType> ProductTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

      Expression<Func<BaseAuditableEntity, bool>> filterExpr = bm => !bm.IsDeleted;
      foreach (var mutableEntityType in modelBuilder.Model.GetEntityTypes())
      {
        // check if current entity type is child of BaseModel
        if (mutableEntityType.ClrType.IsAssignableTo(typeof(BaseAuditableEntity)))
        {
          // modify expression to handle correct child type
          var parameter = Expression.Parameter(mutableEntityType.ClrType);
          var body = ReplacingExpressionVisitor.Replace(filterExpr.Parameters.First(), parameter, filterExpr.Body);
          var lambdaExpression = Expression.Lambda(body, parameter);

          // set filter
          mutableEntityType.SetQueryFilter(lambdaExpression);
        }
      }
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