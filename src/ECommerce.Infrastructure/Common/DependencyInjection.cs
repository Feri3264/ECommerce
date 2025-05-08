using ECommerce.Application.Common.Auth;
using ECommerce.Application.Common.Interfaces.Repositories;
using ECommerce.Infrastructure.Address.Persistance;
using ECommerce.Infrastructure.Common.Auth;
using ECommerce.Infrastructure.Common.Persistence;
using ECommerce.Infrastructure.Group.Persistance;
using ECommerce.Infrastructure.OrderItem.Persistance;
using ECommerce.Infrastructure.Product.Persistance;
using ECommerce.Infrastructure.Shopcart.Persistance;
using ECommerce.Infrastructure.ShopcartProductMapper.Persistence;
using ECommerce.Infrastructure.Subgroup.Persistance;
using ECommerce.Infrastructure.User.Persistance;
using ECommerce.Infrastructure.Wishlist.Persistance;
using ECommerce.Infrastructure.WishlistProductMapper.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Infrastructure.Common;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services , IConfiguration configuration)
    {
        //DbContext
        services.AddDbContext<ECommerceDbContext>(option => 
            option.UseSqlServer(configuration.GetConnectionString("ECommerceConnection")));
        
        //Repository
        services.AddScoped<IAddressRepository, AddressRepository>();
        services.AddScoped<IGroupRepository, GroupRepository>();
        services.AddScoped<IOrderItemRepository, OrderItemRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IShopcartRepository, ShopcartRepository>();
        services.AddScoped<ISubgroupRepository, SubgroupRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IWishlistRepository, WishlistRepository>();
        services.AddScoped<IWishlistProductMapperReository, WishlistProductMapperRepository>();
        services.AddScoped<IShopcartProductMapperRepository, ShopcartProductMapperRepository>();
        
        //Services
        services.AddScoped<IPasswordService, PasswordService>();
        
        return services;
    }
}