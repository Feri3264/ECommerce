using ECommerce.Domain.Address;
using ECommerce.Domain.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Address.Persistence;

internal class AddressConfiguration : IEntityTypeConfiguration<AddressModel>
{
    public void Configure(EntityTypeBuilder<AddressModel> builder)
    {
        //properties
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).ValueGeneratedNever();
        builder.Property(a => a.Id).HasColumnType("uniqueidentifier");
        
        builder.Property(a => a.Country).HasMaxLength(50).IsRequired();
        builder.Property(a => a.City).HasMaxLength(50).IsRequired();
        builder.Property(a => a.Plate).HasMaxLength(20).IsRequired();
        builder.Property(a => a.Street).HasMaxLength(50);
        builder.Property(a => a.Alley).HasMaxLength(50);
        builder.Property(a => a.UserId).HasColumnType("uniqueidentifier").IsRequired();
        
        //Relations
        builder.HasOne<UserModel>()
            .WithMany()
            .HasForeignKey(a => a.UserId);
    }
}