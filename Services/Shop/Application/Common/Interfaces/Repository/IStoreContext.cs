using Microsoft.EntityFrameworkCore;
using Shop.Core.Entities;
using Shop.Core.Entities.OrderAggregate;

namespace Shop.Application.Common.Interfaces.Repository;

public interface IStoreContext
{
    public DbSet<Audit> Audits { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<ProductComputer> ProductComputer { get; set; }

    public DbSet<ProductRealEstate> ProductRealEstate { get; set; }

    public DbSet<ProductVehicle> ProductVehicle { get; set; }

    public DbSet<ProductBrand> ProductBrands { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<City> Cities { get; set; }

    public DbSet<County> Counties { get; set; }

    public DbSet<Currency> Currency { get; set; }

    public DbSet<Favourite> Favourites { get; set; }

    public DbSet<Message> Messages { get; set; }

    public DbSet<Group> Groups { get; set; }

    public DbSet<Connection> Connections { get; set; }

    public DbSet<Order> Orders { get; set; }

    public DbSet<OrderItem> OrderItems { get; set; }

    public DbSet<DeliveryMethod> DeliveryMethods { get; set; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
