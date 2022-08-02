using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using System.Reflection;

namespace Infrastructure.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
        }

        public DbSet<AppUser> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductMachine> ProductMachines { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<County> Counties { get; set; }

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

            modelBuilder.Entity<Product>()
                .HasOne(b => b.ProductMachine)
                .WithOne(b => b.Product).HasForeignKey<ProductMachine>(x => x.Id);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
              .Entries()
              .Where(e => e.Entity is FullAuditableEntity &&
                    (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((FullAuditableEntity)entityEntry.Entity).LastUpdated = DateTime.Now;

                // if (entityEntry.State == EntityState.Added)
                // {
                //   ((BaseAuditableEntity)entityEntry.Entity).CreationDate = DateTime.Now;
                // }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}