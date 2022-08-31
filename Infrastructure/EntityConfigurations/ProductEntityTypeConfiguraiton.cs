using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;

internal class ProductEntityTypeConfiguraiton : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> productConfiguration)
    {
        productConfiguration
            .HasOne(b => b.ProductMachine)
            .WithOne(b => b.Product).HasForeignKey<ProductMachine>(x => x.Id);

        productConfiguration.Property(p => p.Id).IsRequired();
        productConfiguration.Property(p => p.Name).IsRequired().HasMaxLength(100);
        productConfiguration.Property(p => p.Description).IsRequired();
        productConfiguration.Property(p => p.Price).HasColumnType("decimal(18,2)");
        productConfiguration.HasOne(p => p.ProductBrand).WithMany()
            .HasForeignKey(p => p.ProductBrandId);
        productConfiguration.HasOne(p => p.Category).WithMany()
            .HasForeignKey(p => p.CategoryId);
    }
}
