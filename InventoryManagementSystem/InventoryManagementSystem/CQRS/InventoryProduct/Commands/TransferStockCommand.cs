namespace InventoryManagementSystem.CQRS.InventoryProduct.Commands
{
    public class TransferStockCommand : IRequest<ResponseDTO<TransferStockDTO>>
    {
        public TransferStockCommand(TransferStockDTO transferStock)
        {
            TransferStock = transferStock;
        }
        public TransferStockDTO TransferStock { get; }
    }
    public class TransferStockCommandHandler : IRequestHandler<TransferStockCommand, ResponseDTO<TransferStockDTO>>
    {
        private readonly IGenericRepository<ProductInventory> repository;

        public TransferStockCommandHandler(IGenericRepository<ProductInventory> repository)
        {
            this.repository = repository;
        }
        public async Task<ResponseDTO<TransferStockDTO>> Handle(TransferStockCommand request, CancellationToken cancellationToken)
        {
            var dto = request.TransferStock;

            //ensure we have this product 
            var sourceInventory = await repository.Get(
                p => p.productID == dto.ProductId &&
                p.WarehousesID == dto.warehousesIDFrom
                )
                .FirstOrDefaultAsync();

            if (sourceInventory == null || sourceInventory.Quantity < dto.Quantity)
            {
                return ResponseDTO<TransferStockDTO>.Error(ErrorCode.ServerError, "Insufficient stock in source warehouse");
            }

            var targetInventory = await repository.Get(
                p => p.productID == dto.ProductId &&
                p.WarehousesID == dto.warehousesIDTo
               ).FirstOrDefaultAsync();

            if (targetInventory == null)
            {
                targetInventory = new ProductInventory
                {
                    productID = dto.ProductId,
                    WarehousesID = dto.warehousesIDTo,
                    Quantity = 0
                };

                await repository.InsertAsync(targetInventory);
                await repository.SaveAsync();
            }


            sourceInventory.Quantity -= dto.Quantity;
            targetInventory.Quantity += dto.Quantity;

            repository.Update(sourceInventory);
            repository.Update(targetInventory);

            await repository.SaveAsync();


            if (sourceInventory.Quantity < sourceInventory.LowStockThreshold)
            {
                BackgroundJob.Enqueue(() => Console.WriteLine($"Product ID {sourceInventory.productID} is low on stock. Quantity: {sourceInventory.Quantity}, Threshold: {sourceInventory.LowStockThreshold}"));
            }

                return ResponseDTO<TransferStockDTO>.Success(dto, "Stock transferred successfully");

            }
        }
    }

