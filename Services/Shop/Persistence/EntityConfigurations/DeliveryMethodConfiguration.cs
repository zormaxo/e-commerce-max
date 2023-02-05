using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Core.Entities.OrderAggregate;

namespace Shop.Persistence.EntityConfigurations;

public class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
{
    public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
    { builder.Property(d => d.Price).HasColumnType("decimal(18,2)"); }
}