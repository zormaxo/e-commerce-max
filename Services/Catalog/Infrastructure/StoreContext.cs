using Application.Entities;
using Core.Entities;
using EntityFrameworkNet6Tre.Data;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using System.Reflection;

namespace Application;

public class StoreContext : DbContext
{
    private readonly int _userId;
    private bool processAudit;

    public StoreContext(DbContextOptions<StoreContext> options, UserResolverService userService) : base(options)
    {
        _userId = userService.GetUserId();
    }

    public DbSet<Audit> Audits { get; set; }
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

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        if (ChangeTracker.Entries().Any(x => x.Entity is Audit && x.State != EntityState.Unchanged))
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
        else
        {
            var auditEntries = OnBeforeSaveChanges();
            var saveResult = await base.SaveChangesAsync(cancellationToken);

            if (auditEntries.Count > 0)
                await OnAfterSaveChanges(auditEntries);

            return saveResult;
        }
    }

    private List<AuditEntry> OnBeforeSaveChanges()
    {
        var entries = ChangeTracker
          .Entries()
          .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted);

        var auditEntries = new List<AuditEntry>();

        foreach (var entityEntry in entries)
        {
            if (entityEntry.Entity is FullAuditableEntity fullAuditableEntity)
            {
                //var fullAuditableEntity =  (FullAuditableEntity)entityEntry.Entity;

                fullAuditableEntity.ModifiedDate = DateTime.Now;
                fullAuditableEntity.ModifiedBy = _userId;

                if (entityEntry.State == EntityState.Added)
                {
                    fullAuditableEntity.CreatedDate = DateTime.Now;
                    fullAuditableEntity.ModifiedBy = _userId;
                }
            }

            var auditEntry = new AuditEntry(entityEntry)
            {
                TableName = entityEntry.Metadata.GetTableName(),
                Action = entityEntry.State.ToString(),
            };
            auditEntries.Add(auditEntry);

            foreach (var property in entityEntry.Properties)
            {
                if (property.IsTemporary)
                {
                    auditEntry.TemporaryProperties.Add(property);
                    continue;
                }

                string propertyName = property.Metadata.Name;
                if (property.Metadata.IsPrimaryKey())
                {
                    auditEntry.KeyValues[propertyName] = property.CurrentValue;
                    continue;
                }

                switch (entityEntry.State)
                {
                    case EntityState.Added:
                        auditEntry.NewValues[propertyName] = property.CurrentValue;
                        break;

                    case EntityState.Deleted:
                        auditEntry.OldValues[propertyName] = property.OriginalValue;
                        break;

                    case EntityState.Modified:
                        if (property.IsModified)
                        {
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                        }
                        break;
                }
            }
        }

        foreach (var pendingAuditEntry in auditEntries.Where(q => !q.HasTemporaryProperties))
        {
            Audits.Add(pendingAuditEntry.ToAudit(_userId));
        }

        return auditEntries.Where(q => q.HasTemporaryProperties).ToList();
    }

    private Task OnAfterSaveChanges(List<AuditEntry> auditEntries)
    {
        foreach (var auditEntry in auditEntries)
        {
            foreach (var prop in auditEntry.TemporaryProperties)
            {
                if (prop.Metadata.IsPrimaryKey())
                {
                    auditEntry.KeyValues[prop.Metadata.Name] = prop.CurrentValue;
                }
                else
                {
                    auditEntry.NewValues[prop.Metadata.Name] = prop.CurrentValue;
                }
            }
            Audits.Add(auditEntry.ToAudit(_userId));
        }

        return SaveChangesAsync();
    }
}