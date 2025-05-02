using InventoryManagementSystem.CQRS.Transactions.Command;
using InventoryManagementSystem.DTO.TrasactionsDTO;

namespace InventoryManagementSystem.CQRS.Orchestrators
{
    public class RemoveStockOrchestrator:IRequest<ResponseDTO<RemoveStockDTO>>
    {
        public RemoveStockDTO removeStockDTO;

        public RemoveStockOrchestrator(RemoveStockDTO removeStockDTO)
        {
            this.removeStockDTO = removeStockDTO;
        }
    }

    public class RemoveStockOrchestratorHandler : IRequestHandler<RemoveStockOrchestrator, ResponseDTO<RemoveStockDTO>>
    {
        private readonly IMediator mediator;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IHttpContextAccessor httpContextAccessor;


        public RemoveStockOrchestratorHandler(IMediator mediator, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            this.mediator = mediator;
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResponseDTO<RemoveStockDTO>> Handle(RemoveStockOrchestrator request, CancellationToken cancellationToken)
        {
            var dto = request.removeStockDTO;
            var httpContext = httpContextAccessor.HttpContext;
            var user = await userManager.GetUserAsync(httpContext.User);
            if (user == null)
            {
                return ResponseDTO<RemoveStockDTO>.Error(ErrorCode.Unauthorized, "User not found or not logged in.");
            }

            var stockResponse = await mediator.Send(new RemoveStockCommand(request.removeStockDTO), cancellationToken);

            var transactionDto = new AddTransactionDto
            {
                TransactionType = TransactionType.RemoveStock,
                Quantity = dto.Quantity,
                Date = DateTime.UtcNow,
                warehousesIDTo = dto.WarehouseId,
                ProductInventoryID = stockResponse.Data.ProductInventoryID,
                UserId = user.Id
            };

            var transactionResponse = await mediator.Send(new AddTransactionCommand(transactionDto), cancellationToken);

            if (!transactionResponse.IsSuccess)
            {
                return ResponseDTO<RemoveStockDTO>.Error(ErrorCode.ServerError, "Stock added but transaction failed.");
            }

            return ResponseDTO<RemoveStockDTO>.Success(request.removeStockDTO, "Stock and transaction added successfully");

        }
    }

}
