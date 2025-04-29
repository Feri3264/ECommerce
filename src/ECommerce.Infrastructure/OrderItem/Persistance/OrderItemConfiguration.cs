using ECommerce.Domain.OrderItem;
using ECommerce.Domain.Product;
using ECommerce.Domain.Shopcart;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.OrderItem.Persistance;

internal class OrderItemConfiguration : IEntityTypeConfiguration<OrderItemModel>
{
    public void Configure(EntityTypeBuilder<OrderItemModel> builder)
    {
        //properties
        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id).ValueGeneratedNever();
        builder.Property(o => o.Id).HasColumnType("uniqueidentifier");
        
        builder.Property(o => o.Name).HasMaxLength(200).IsRequired();
        builder.Property(o => o.Price).HasColumnType("Money").IsRequired();
        builder.Property(o => o.Quantity).IsRequired();
        builder.Property(o => o.TotalOrderPrice).HasColumnType("Money").IsRequired();
        
        builder.Property(o => o.ProductId)
            .HasColumnType("uniqueidentifier").IsRequired();
        
        builder.Property(o => o.ShopcartId)
            .HasColumnType("uniqueidentifier").IsRequired();
        
        //Relations
        builder.HasOne<ProductModel>()
            .WithMany()
            .HasForeignKey(o => o.ProductId);
        
        builder.HasOne<ShopcartModel>()
            .WithMany()
            .HasForeignKey(o => o.ShopcartId);
    }
}