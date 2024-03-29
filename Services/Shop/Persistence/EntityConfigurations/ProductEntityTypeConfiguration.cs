﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Core.Entities;

namespace Shop.Persistence.EntityConfigurations;

internal class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> productConfiguration)
    {
        productConfiguration.Property(p => p.Id).IsRequired();
        productConfiguration.Property(p => p.Name).IsRequired().HasMaxLength(100);
        productConfiguration.Property(p => p.Description).IsRequired();
        productConfiguration.Property(p => p.Price).HasColumnType("decimal(18,2)");
        productConfiguration.HasOne(p => p.Category).WithMany().HasForeignKey(p => p.CategoryId);
    }
}
