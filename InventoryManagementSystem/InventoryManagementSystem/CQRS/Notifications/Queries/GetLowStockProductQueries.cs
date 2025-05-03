namespace InventoryManagementSystem.CQRS.Notifications.Commands
{
    public class GetLowStockProductQueries:IRequest<List<GetLowStockProductDTO>>
    {
    }
    public class GetLowStockProductQueriesHandler : IRequestHandler<GetLowStockProductQueries, List<GetLowStockProductDTO>>
    {
        private readonly IGenericRepository<ProductInventory> repository;

        public GetLowStockProductQueriesHandler(IGenericRepository<ProductInventory> repository)
        {
            this.repository = repository;
        }
        public async Task<List<GetLowStockProductDTO>> Handle(GetLowStockProductQueries request, CancellationToken cancellationToken)
        {
            var lowStockItems = await repository.Get(
             p => p.Quantity < p.LowStockThreshold)
                .projectTo<GetLowStockProductDTO>()
                .ToListAsync();

            return lowStockItems ;

            //return lowStockItems.Map<List<GetLowStockProductDTO>>();
        }
    }
 }
