using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Warehouse.Core.Common;
using Warehouse.Core.Entities;
using Warehouse.Shared.Services;
using WarehouseEntity = Warehouse.Core.Entities.Warehouse;

namespace Warehouse.DataAccess.Persistence;

public class DatabaseContext : DbContext
{
    private readonly IClaimService _claimService;

    public DatabaseContext(DbContextOptions<DatabaseContext> options, IClaimService claimService) : base(options)
    {
        _claimService = claimService;
    }

    public DbSet<WarehouseEntity> Warehouses { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<WarehouseItem> WarehouseItems { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        builder.Entity<WarehouseEntity>()
            .HasIndex(w => w.Name)
            .IsUnique();

        builder.Entity<Item>()
            .HasIndex(i => new { i.Name, i.WarehouseId })
            .IsUnique();

        builder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        base.OnModelCreating(builder);
    }

    public new async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<IAuditedEntity>())
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = _claimService.GetUserId();
                    entry.Entity.CreatedOn = DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedBy = _claimService.GetUserId();
                    entry.Entity.UpdatedOn = DateTime.UtcNow;
                    break;
            }

        return await base.SaveChangesAsync(cancellationToken);
    }
}
