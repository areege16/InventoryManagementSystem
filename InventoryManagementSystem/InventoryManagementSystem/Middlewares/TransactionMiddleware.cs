
using Microsoft.EntityFrameworkCore.Storage;

namespace InventoryManagementSystem.Middlewares
{
    public class TransactionMiddleware : IMiddleware
    {
        private readonly InventoryContext context;
        private readonly ILogger<TransactionMiddleware> logger;

        public TransactionMiddleware(InventoryContext context , ILogger<TransactionMiddleware> logger )
        {
            this.context = context;
            this.logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (context.Request.Method == "GET")
            {
                await next(context);
                return;
            }

            IDbContextTransaction transaction = null;

            try
            {
                transaction = await this.context.Database.BeginTransactionAsync();

                await next(context);

                if (transaction != null)
                    await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                if (transaction != null)
                    await transaction.RollbackAsync();

                logger.LogError(ex, "Transaction failed due to erro");

                throw;
            }
        }
    }
}
