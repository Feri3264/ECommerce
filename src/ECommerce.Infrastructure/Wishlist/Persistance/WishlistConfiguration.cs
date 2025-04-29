using ECommerce.Domain.Product;
using ECommerce.Domain.User;
using ECommerce.Domain.Wishlist;
using ECommerce.Domain.WishlistProductMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Wishlist.Persistance;

internal class WishlistConfiguration : IEntityTypeConfiguration<WishlistModel>
{
    public void Configure(EntityTypeBuilder<WishlistModel> builder)
    {
        //properties
        builder.HasKey(w => w.Id);
        builder.Property(w => w.Id).ValueGeneratedNever();
        builder.Property(w => w.UserId).IsRequired();
        builder.Property(w => w.Id).HasColumnType("uniqueidentifier");
        
        builder.Property(w => w.IsDelete).IsRequired();
        
        builder.Property(w => w.UserId)
            .HasColumnType("uniqueidentifier").IsRequired();
        
        //Relations
        builder.HasOne<UserModel>()
            .WithOne()
            .HasForeignKey<WishlistModel>(w => w.UserId);

        builder.HasMany<WishlistProductMapperModel>()
            .WithOne()
            .HasForeignKey(w => w.WishlistId);
    }
}