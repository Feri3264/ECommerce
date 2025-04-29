using ECommerce.Domain.Product;
using ECommerce.Domain.Shopcart;
using ECommerce.Domain.ShopcartProductMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.ShopcartProductMapper.Persistence;

internal class ShopcartProductMapperConfiguration : IEntityTypeConfiguration<ShopcartProductMapperModel>
{
    public void Configure(EntityTypeBuilder<ShopcartProductMapperModel> builder)
    {
        //properties
        builder.HasKey(x => x.Id);
        builder.Property(s => s.Id).ValueGeneratedNever();
        builder.Property(s => s.Id).HasColumnType("uniqueidentifier");
        
        builder.Property(s => s.ShopcartId)
            .HasColumnType("uniqueidentifier").IsRequired();
        
        builder.Property(s => s.ProductId)
            .HasColumnType("uniqueidentifier").IsRequired();
        
        
        //Relations
        builder.HasOne<ProductModel>()
            .WithMany()
            .HasForeignKey(s => s.ProductId);
        
        builder.HasOne<ShopcartModel>()
            .WithMany()
            .HasForeignKey(s => s.ShopcartId);
    }
}