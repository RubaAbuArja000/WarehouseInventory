using Warehouse.DataAccess.Persistence;

namespace WarehouseInventory.Middleware;

public class TransactionMiddleware(RequestDelegate next, ILogger<TransactionMiddleware> logger)
{
    public async Task Invoke(HttpContext context, DatabaseContext databaseContext)
    {
        await using var transaction = await databaseContext.Database.BeginTransactionAsync();

        try
        {
            await next(context);
            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            logger.LogError(ex, "Transaction rolled back due to exception.");
            throw;
        }
    }
}
