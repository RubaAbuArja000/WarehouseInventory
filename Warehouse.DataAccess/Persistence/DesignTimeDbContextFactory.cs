using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Warehouse.Shared.Services;

namespace Warehouse.DataAccess.Persistence;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
{
    public DatabaseContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();

        optionsBuilder.UseSqlServer(
            "Server=RABUARJA-PC;Database=WarehouseInventoryDb;Trusted_Connection=True;" +
            "TrustServerCertificate=True;MultipleActiveResultSets=true");

        return new DatabaseContext(optionsBuilder.Options, new NullClaimService());
    }

    private sealed class NullClaimService : IClaimService
    {
        public string GetUserId() => string.Empty;
        public string GetClaim(string key) => string.Empty;
    }
}
