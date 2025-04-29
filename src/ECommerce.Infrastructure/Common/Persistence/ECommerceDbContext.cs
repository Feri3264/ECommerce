using System.Reflection;
using ECommerce.Domain.Address;
using ECommerce.Domain.Group;
using ECommerce.Domain.OrderItem;
using ECommerce.Domain.Product;
using ECommerce.Domain.Shopcart;
using ECommerce.Domain.ShopcartProductMapper;
using ECommerce.Domain.Subgroup;
using ECommerce.Domain.User;
using ECommerce.Domain.Wishlist;
using ECommerce.Domain.WishlistProductMapper;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Common.Persistence;

public class ECommerceDbContext
    (DbContextOptions<ECommerceDbContext> options) : DbContext(options)
{
    public DbSet<UserModel> Users { get; set; }
    public DbSet<WishlistModel> Wishlists { get; set; }
    public DbSet<SubgroupModel> Subgroups { get; set; }
    public DbSet<ShopcartModel> Shopcarts { get; set; }
    public DbSet<ProductModel> Products { get; set; }
    public DbSet<ShopcartProductMapperModel> ShopcartProductMappers { get; set; }
    public DbSet<OrderItemModel> OrderItems { get; set; }
    public DbSet<WishlistProductMapperModel> WishlistProductMappers { get; set; }
    public DbSet<GroupModel> Groups { get; set; }
    public DbSet<AddressModel> Addresses { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        base.OnModelCreating(modelBuilder);
    }
    
}