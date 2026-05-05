using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Warehouse.Core.IRepositories;
using Warehouse.DataAccess.Persistence;
using Warehouse.DataAccess.Repositories;

namespace Warehouse.DataAccess;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DatabaseContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IWarehouseRepository, WarehouseRepository>();
        services.AddScoped<IItemRepository, ItemRepository>();
        services.AddScoped<IWarehouseItemRepository, WarehouseItemRepository>();

        return services;
    }
}
