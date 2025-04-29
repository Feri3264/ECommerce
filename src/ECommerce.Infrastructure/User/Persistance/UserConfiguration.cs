using ECommerce.Domain.Address;
using ECommerce.Domain.Shopcart;
using ECommerce.Domain.User;
using ECommerce.Domain.Wishlist;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.User.Persistance;

internal class UserConfiguration : IEntityTypeConfiguration<UserModel>
{
    public void Configure(EntityTypeBuilder<UserModel> builder)
    {
        //properties
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).ValueGeneratedNever();
        builder.Property(u => u.Id).HasColumnType("uniqueidentifier");
        
        builder.Property(u => u.Name).HasMaxLength(50).IsRequired();
        builder.Property(u => u.Email).HasMaxLength(150).IsRequired();
        builder.Property(u => u.Username).HasMaxLength(100).IsRequired();
        builder.Property(u => u.Password).HasMaxLength(150).IsRequired();
        builder.Property(u => u.IsAdmin).IsRequired();
        builder.Property(u => u.IsEditor).IsRequired();
        builder.Property(u => u.IsDelete).IsRequired();
        
        builder.Property(u => u.WishlistId)
            .HasColumnType("uniqueidentifier").IsRequired();
    }
}