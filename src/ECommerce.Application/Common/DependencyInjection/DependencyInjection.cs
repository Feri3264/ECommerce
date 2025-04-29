using ECommerce.Application.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Application.Common.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(options =>
            options.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection)));
        
        
        return services;
    }
}