
namespace InventoryManagementSystem.CQRS.InventoryProduct.Commands
{
    public class RemoveStockCommand:IRequest<ResponseDTO<RemoveStockDTO>>
    {
        public RemoveStockCommand(RemoveStockDTO removeStock)
        {
            RemoveStock = removeStock;
        }
        public RemoveStockDTO RemoveStock { get; }
    }
    public class RemoveStockCommandHandler : IRequestHandler<RemoveStockCommand, ResponseDTO<RemoveStockDTO>>
        {
        private readonly IGenericRepository<ProductInventory> repository;
        public RemoveStockCommandHandler(IGenericRepository<ProductInventory> repository)
        {
            this.repository = repository;
        }
        public async Task<ResponseDTO<RemoveStockDTO>> Handle(RemoveStockCommand request, CancellationToken cancellationToken)
        {
            var DTO = request.RemoveStock;

            var ExistingProducInventory = await repository.Get(
                i => i.productID == DTO.ProductId &&
                i.WarehousesID == DTO.WarehouseId
                )
                .FirstOrDefaultAsync();

            if (ExistingProducInventory != null)
            {
                ExistingProducInventory.Quantity -= DTO.Quantity;
                repository.Update(ExistingProducInventory);
                await repository.SaveAsync();

                DTO.ProductInventoryID = ExistingProducInventory.id;

                if (ExistingProducInventory.Quantity < ExistingProducInventory.LowStockThreshold)
                {
                    BackgroundJob.Enqueue(() => Console.WriteLine($"Product ID {ExistingProducInventory.productID} is low on stock. Quantity: {ExistingProducInventory.Quantity}, Threshold: {ExistingProducInventory.LowStockThreshold}"));
                }

                return ResponseDTO<RemoveStockDTO>.Success(DTO, "Updated quantity successfully");
            }
            else
            {
                return ResponseDTO<RemoveStockDTO>.Error(ErrorCode.NotFound, "Product not found in the selected warehouse. Please add it first");
            }

        }
    }
}
