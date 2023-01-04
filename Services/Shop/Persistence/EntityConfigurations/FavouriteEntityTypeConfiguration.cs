using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Core.Entities;

namespace Shop.Persistence.EntityConfigurations;

internal class FavouriteEntityTypeConfiguration : IEntityTypeConfiguration<Favourite>
{
    public void Configure(EntityTypeBuilder<Favourite> modelBuilder)
    {
        //many to many relation
        modelBuilder.HasKey(ba => new { ba.LikedProductId, ba.UserId });
        modelBuilder
          .HasOne(b => b.User)
            .WithMany(b => b.Favorites)
            .HasForeignKey(b => b.UserId);
        modelBuilder
          .HasOne(b => b.LikedProduct)
            .WithMany(b => b.Favourites)
            .HasForeignKey(b => b.LikedProductId);
    }
}
