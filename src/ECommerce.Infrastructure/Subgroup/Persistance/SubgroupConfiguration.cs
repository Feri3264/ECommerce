using ECommerce.Domain.Group;
using ECommerce.Domain.Product;
using ECommerce.Domain.Subgroup;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Subgroup.Persistance;

internal class SubgroupConfiguration : IEntityTypeConfiguration<SubgroupModel>
{
    public void Configure(EntityTypeBuilder<SubgroupModel> builder)
    { 
        //properties
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).ValueGeneratedNever();
        builder.Property(s => s.Id).HasColumnType("uniqueidentifier");
        
        builder.Property(s => s.Name).HasMaxLength(100).IsRequired();
        builder.Property(s => s.IsDelete).IsRequired();
        
        builder.Property(s => s.GroupId)
            .HasColumnType("uniqueidentifier").IsRequired();
        
        //Relations
        builder.HasOne<GroupModel>()
            .WithMany()
            .HasForeignKey(s => s.GroupId);
        
        //seed date
        builder.HasData(
            new SubgroupModel(
                _name: "uncategorized",
                _createDate: DateTime.Today,
                _modifiedDate: DateTime.Today,
                id: Guid.Parse("d859e766-fb34-4914-9c8d-27b02c40ffd4"),
                _groupId: Guid.Parse("2a308c81-0485-49a2-8822-a8d61a981093")));
    }
}