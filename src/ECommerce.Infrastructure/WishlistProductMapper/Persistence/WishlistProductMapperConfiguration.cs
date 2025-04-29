using ECommerce.Domain.Product;
using ECommerce.Domain.Shopcart;
using ECommerce.Domain.Wishlist;
using ECommerce.Domain.WishlistProductMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.WishlistProductMapper.Persistence;

internal class WishlistProductMapperConfiguration : IEntityTypeConfiguration<WishlistProductMapperModel>
{
    public void Configure(EntityTypeBuilder<WishlistProductMapperModel> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).ValueGeneratedNever();
        builder.Property(s => s.Id).HasColumnType("uniqueidentifier");
        
        //Relations
        builder.HasOne<ProductModel>()
            .WithMany()
            .HasForeignKey(s => s.ProductId);

        builder.HasOne<WishlistModel>()
            .WithMany()
            .HasForeignKey(s => s.WishlistId);
    }
}