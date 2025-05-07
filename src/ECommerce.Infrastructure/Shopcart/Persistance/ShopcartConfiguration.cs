using ECommerce.Domain.Address;
using ECommerce.Domain.OrderItem;
using ECommerce.Domain.Shopcart;
using ECommerce.Domain.ShopcartProductMapper;
using ECommerce.Domain.User;
using ECommerce.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Shopcart.Persistance;

internal class ShopcartConfiguration : IEntityTypeConfiguration<ShopcartModel>
{
    public void Configure(EntityTypeBuilder<ShopcartModel> builder)
    {
        //properties
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).ValueGeneratedNever();
        builder.Property(s => s.Id).HasColumnType("uniqueidentifier");

        builder.Property(s => s.TotalPrice).HasColumnType("Money").IsRequired();
        builder.Property(s => s.UserId).HasColumnType("uniqueidentifier").IsRequired();
        builder.Property(s => s.ShopcartProductId).HasListOfIdsConverter();
        builder.Property(s => s.OrderItemIds).HasListOfIdsConverter();
        
        //Relations
        builder.HasOne<UserModel>()
            .WithOne()
            .HasForeignKey<ShopcartModel>(s => s.UserId);
        
        builder.HasOne<AddressModel>()
            .WithOne()
            .HasForeignKey<ShopcartModel>(s => s.AddressId);
    }
}