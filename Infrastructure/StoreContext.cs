using Application.Entities;
using Core.Entities;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using System.Reflection;

namespace Application;

public class StoreContext : DbContext
{
    private readonly UserResolverService _userService;

    public StoreContext(DbContextOptions<StoreContext> options, UserResolverService userService) : base(options)
    {
        _userService = userService;
    }

    public DbSet<AppUser> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductMachine> ProductMachines { get; set; }
    public DbSet<ProductBrand> ProductBrands { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<County> Counties { get; set; }
    public DbSet<Currency> Currency { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //e-commerce 27
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        Expression<Func<BaseAuditableEntity, bool>> filterExpr = bm => !bm.IsDeleted;
        foreach (var mutableEntityType in modelBuilder.Model.GetEntityTypes().Where(
        // check if current entity type is child of BaseModel
        mutableEntityType => mutableEntityType.ClrType.IsAssignableTo(typeof(BaseAuditableEntity))))
        {
            // modify expression to handle correct child type
            var parameter = Expression.Parameter(mutableEntityType.ClrType);
            var body = ReplacingExpressionVisitor.Replace(filterExpr.Parameters[0], parameter, filterExpr.Body);
            var lambdaExpression = Expression.Lambda(body, parameter);
            // set filter
            mutableEntityType.SetQueryFilter(lambdaExpression);
        }

        //e-commerce 62
        if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.ClrType.GetProperties().Where(p => p.PropertyType == typeof(decimal)))
                {
                    modelBuilder.Entity(entityType.Name).Property(property.Name).HasConversion<double>();
                }
            }
        }
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker
          .Entries()
          .Where(e => e.Entity is FullAuditableEntity &&
                (e.State == EntityState.Added || e.State == EntityState.Modified));

        foreach (var entityEntry in entries)
        {
            var fullAuditableEntity = (FullAuditableEntity)entityEntry.Entity;
            var userId = _userService.GetUserId();

            fullAuditableEntity.ModifiedDate = DateTime.Now;
            fullAuditableEntity.ModifiedBy = userId;

            if (entityEntry.State == EntityState.Added)
            {
                fullAuditableEntity.CreatedDate = DateTime.Now;
                fullAuditableEntity.ModifiedBy = userId;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}