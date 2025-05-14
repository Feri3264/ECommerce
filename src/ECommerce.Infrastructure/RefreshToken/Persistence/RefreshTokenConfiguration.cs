using ECommerce.Domain.RefreshToken;
using ECommerce.Domain.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.RefreshToken.Persistence;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshTokenModel>
{
    public void Configure(EntityTypeBuilder<RefreshTokenModel> builder)
    {
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).ValueGeneratedNever();
        builder.Property(r => r.Id).HasColumnType("uniqueidentifier");
        
        //Relations
        builder.HasOne<UserModel>()
            .WithOne()
            .HasForeignKey<RefreshTokenModel>(r => r.UserId);
    }
}