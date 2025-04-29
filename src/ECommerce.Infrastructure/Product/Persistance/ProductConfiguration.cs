using ECommerce.Domain.OrderItem;
using ECommerce.Domain.Product;
using ECommerce.Domain.Shopcart;
using ECommerce.Domain.ShopcartProductMapper;
using ECommerce.Domain.Subgroup;
using ECommerce.Domain.WishlistProductMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Product.Persistance;

internal class ProductConfiguration : IEntityTypeConfiguration<ProductModel>
{
    public void Configure(EntityTypeBuilder<ProductModel> builder)
    {
        //properties
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).ValueGeneratedNever();
        builder.Property(p => p.Id).HasColumnType("uniqueidentifier");
        
        builder.Property(p => p.Name).HasMaxLength(200).IsRequired();
        builder.Property(p => p.Price).HasColumnType("Money").IsRequired();
        builder.Property(p => p.ShortDesc).HasMaxLength(500).IsRequired();
        builder.Property(p => p.FullDesc).IsRequired();
        builder.Property(p => p.AllowUserComments).IsRequired();
        builder.Property(p => p.IsDelete).IsRequired();
        
        builder.Property(p => p.SubgroupId)
            .HasColumnType("uniqueidentifier").IsRequired();
        
        //Relations
        builder.HasOne<SubgroupModel>()
            .WithMany()
            .HasForeignKey(p => p.SubgroupId);
    }
}