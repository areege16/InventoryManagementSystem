
namespace InventoryManagementSystem.Jobs
{
    public class DailyNotification
    {
        private readonly IMediator mediator;
        private readonly IGenericRepository<Notification> repository;

        public DailyNotification(IMediator mediator,IGenericRepository<Notification> repository)
        {
            this.mediator = mediator;
            this.repository = repository;
        }

        public async Task DailyCheckLowStockAsync()
        {
            var lowStockItems = await mediator.Send(new GetLowStockProductQueries());

            foreach (var item in lowStockItems)
            {
                var message = ($"Product ID {item.productID} is low on stock. Quantity: {item.Quantity}, Threshold: {item.LowStockThreshold}");
                var notification = new Notification
                {
                    productInventoryID = item.productInventoryID,
                    Message = message,
                    CreatedAt = DateTime.Now
                };

                await repository.InsertAsync(notification);
            }
            await repository.SaveAsync();
        }
    }
}
