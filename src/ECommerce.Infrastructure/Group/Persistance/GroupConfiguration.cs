using ECommerce.Domain.Group;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Group.Persistance;

internal class GroupConfiguration : IEntityTypeConfiguration<GroupModel>
{
    public void Configure(EntityTypeBuilder<GroupModel> builder)
    {
        //properties
        builder.HasKey(g => g.Id);
        builder.Property(g => g.Id).ValueGeneratedNever();
        builder.Property(g => g.Id).HasColumnType("uniqueidentifier");
        
        builder.Property(g => g.Name).HasMaxLength(100).IsRequired();
        builder.Property(g => g.IsDelete).IsRequired();
        
        
        //seed data
        builder.HasData(
            new GroupModel(
                _name: "uncategorized",
                _createDate: DateTime.Today, 
                _modifiedDate: DateTime.Today,
                id: Guid.Parse("2a308c81-0485-49a2-8822-a8d61a981093")));
    }
}