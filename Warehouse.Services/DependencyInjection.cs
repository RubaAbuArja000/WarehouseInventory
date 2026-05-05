using Microsoft.Extensions.DependencyInjection;
using Warehouse.Shared.Services;

namespace Warehouse.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<IClaimService, ClaimService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IWarehouseService, WarehouseService>();
        services.AddScoped<IWarehouseItemService, WarehouseItemService>();
        services.AddScoped<IItemService, ItemService>();
        services.AddScoped<IDashboardService, DashboardService>();

        return services;
    }
}