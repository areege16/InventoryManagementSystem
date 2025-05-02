using InventoryManagementSystem.CQRS.Transactions.Command;
using InventoryManagementSystem.DTO.TrasactionsDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace InventoryManagementSystem.CQRS.Orchestrators
{
    public class TransferStockOrchestrator:IRequest<ResponseDTO<TransferStockDTO>>
    {
        public TransferStockOrchestrator(TransferStockDTO transferStockDTO)
        {
            TransferStockDTO = transferStockDTO;
        }
        public TransferStockDTO TransferStockDTO { get; }
    }

    public class TransferStockOrchestratorHandler : IRequestHandler<TransferStockOrchestrator, ResponseDTO<TransferStockDTO>>
    {
        private readonly IMediator mediator;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IHttpContextAccessor httpContextAccessor;

        public TransferStockOrchestratorHandler(IMediator mediator, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            this.mediator = mediator;
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResponseDTO<TransferStockDTO>> Handle(TransferStockOrchestrator request, CancellationToken cancellationToken)
        {
            var httpContext = httpContextAccessor.HttpContext;
            var user = await userManager.GetUserAsync(httpContext.User);
            if (user == null)
            {
                return ResponseDTO<TransferStockDTO>.Error(ErrorCode.Unauthorized, "User not found or not logged in.");
            }

            var transferResult = await mediator.Send(new TransferStockCommand(request.TransferStockDTO), cancellationToken);


            var transactionDto = new TransferTransactionDto
            {
                TransactionType = TransactionType.Transfer,
                Quantity = request.TransferStockDTO.Quantity,
                Date = DateTime.UtcNow,
                UserId = user.Id,
                productInventoryID = request.TransferStockDTO.productInventoryID,
                warehousesIDFrom = request.TransferStockDTO.warehousesIDFrom,
                warehousesIDTo = request.TransferStockDTO.warehousesIDTo
            };
            var transactionResponse = await mediator.Send(new AddTransferTransactionCommand(transactionDto), cancellationToken);

            if (!transactionResponse.IsSuccess)
            {
                return ResponseDTO<TransferStockDTO>.Error(ErrorCode.ServerError, "Stock transfer completed, but transaction logging failed");
            }

            return ResponseDTO<TransferStockDTO>.Success(request.TransferStockDTO, "Stock transferred and transaction recorded successfully");
        }
    }
}
