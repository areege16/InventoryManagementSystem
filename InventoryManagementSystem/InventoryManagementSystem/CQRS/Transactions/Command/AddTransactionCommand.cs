
namespace InventoryManagementSystem.CQRS.Transactions.Command
{
    public class AddTransactionCommand:IRequest<ResponseDTO<AddTransactionDto>>
    {
        public AddTransactionCommand(AddTransactionDto addTransactionDto)
        {
            AddTransactionDto = addTransactionDto;
        }
        public AddTransactionDto AddTransactionDto { get; }
    }

    public class AddTransactionCommandHandler : IRequestHandler<AddTransactionCommand, ResponseDTO<AddTransactionDto>>
    {
        private readonly IGenericRepository<Transaction> repository;

        public AddTransactionCommandHandler(IGenericRepository<Transaction> repository)
        {
            this.repository = repository;
        }
        public async Task<ResponseDTO<AddTransactionDto>> Handle(AddTransactionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var transaction = request.AddTransactionDto.Map<Transaction>();
                await repository.InsertAsync(transaction);
                await repository.SaveAsync();

                var transactionDTO = transaction.Map<AddTransactionDto>();

                return ResponseDTO<AddTransactionDto>.Success(transactionDTO, "transaction done successfully "); 
            }
            catch
            {
                return ResponseDTO<AddTransactionDto>.Error(ErrorCode.ServerError, "error occurred");

            }

        }
    }
}
