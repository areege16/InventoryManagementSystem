
using AutoMapper;
using InventoryManagementSystem.CQRS.Transactions.Command;
using InventoryManagementSystem.DTO.ProductInventoryDto;
using InventoryManagementSystem.DTO.TrasactionsDTO;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.CQRS.Orchestrators
{
    public class AddStockOrchestrator:IRequest<ResponseDTO<AddStockDTO>>
    {
       public AddStockDTO addStockDTO;

        public AddStockOrchestrator(AddStockDTO addStockDTO)
        {
            this.addStockDTO = addStockDTO;
        }
    }
    public class AddStockOrchestratorHandler : IRequestHandler<AddStockOrchestrator, ResponseDTO<AddStockDTO>>
    {
        private readonly IMediator mediator;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IHttpContextAccessor httpContextAccessor;


        public AddStockOrchestratorHandler(IMediator mediator, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            this.mediator = mediator;
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResponseDTO<AddStockDTO>> Handle(AddStockOrchestrator request, CancellationToken cancellationToken)
        {
            var dto = request.addStockDTO;
            var httpContext = httpContextAccessor.HttpContext;
            var user = await userManager.GetUserAsync(httpContext.User);
            if (user == null)
            {
                return ResponseDTO<AddStockDTO>.Error(ErrorCode.Unauthorized, "User not found or not logged in.");
            }

            var stockResponse = await mediator.Send(new AddStockCommand(request.addStockDTO), cancellationToken);


            //var transactionDto = mapper.Map(AddTransactionDto)< request.addStockDTO>;
            var transactionDto = new AddTransactionDto
            {
                TransactionType = TransactionType.AddStock,
                Quantity = dto.Quantity,
                Date = DateTime.UtcNow,
                warehousesIDTo = dto.WarehouseId,
                ProductInventoryID = stockResponse.Data.ProductInventoryID,
                UserId = user.Id
            }; 

            var transactionResponse = await mediator.Send(new AddTransactionCommand(transactionDto), cancellationToken);

            if (!transactionResponse.IsSuccess)
            {
                return ResponseDTO<AddStockDTO>.Error(ErrorCode.ServerError, "Stock added but transaction failed.");
            }

            return ResponseDTO<AddStockDTO>.Success(request.addStockDTO, "Stock and transaction added successfully");

        }
    }
 

}
