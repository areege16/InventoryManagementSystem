
namespace InventoryManagementSystem.CQRS.Transactions.Command
{
    public class AddTransferTransactionCommand: IRequest<ResponseDTO<TransferTransactionDto>>
    {
        public AddTransferTransactionCommand(TransferTransactionDto dto)
        {
            TransferTransactionDto = dto;
        }

        public TransferTransactionDto TransferTransactionDto { get; }
    }

    public class TransferTransactionCommandHandler : IRequestHandler<AddTransferTransactionCommand, ResponseDTO<TransferTransactionDto>>
    {
        private readonly IGenericRepository<Transaction> repository;

        public TransferTransactionCommandHandler(IGenericRepository<Transaction> repository )
        {
            this.repository = repository;
        }
        public async Task<ResponseDTO<TransferTransactionDto>> Handle(AddTransferTransactionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var transaction = request.TransferTransactionDto.Map<Transaction>();

                await repository.InsertAsync(transaction);
                await repository.SaveAsync();

                var dto = transaction.Map<TransferTransactionDto>();

                return ResponseDTO<TransferTransactionDto>.Success(dto, "Transfer transaction successfully");

            }
            catch(Exception ex)
            {
                return ResponseDTO<TransferTransactionDto>.Error(ErrorCode.ServerError, $"Error occurred: {ex.Message}");
            }

        }
    }
}
