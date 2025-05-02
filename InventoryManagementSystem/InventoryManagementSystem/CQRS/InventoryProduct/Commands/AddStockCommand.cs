
namespace InventoryManagementSystem.CQRS.InventoryProduct.Commands
{
    public class AddStockCommand:IRequest<ResponseDTO<AddStockDTO>>
    {
     public AddStockDTO AddStockDTO { get; set; }
        public AddStockCommand(AddStockDTO AddStockDTO)
        {
            this.AddStockDTO = AddStockDTO;
        }
    }
    public class AddStockCommandHandler : IRequestHandler<AddStockCommand, ResponseDTO<AddStockDTO>>
    {
        private readonly IGenericRepository<ProductInventory> repository;

        public AddStockCommandHandler(IGenericRepository<ProductInventory> repository )
        {
            this.repository = repository;
        }
        public async Task<ResponseDTO<AddStockDTO>> Handle(AddStockCommand request, CancellationToken cancellationToken)
        {
            var DTO = request.AddStockDTO;

            var ExistingProducInventory =await repository.Get(
                i => i.productID == DTO.ProductId &&
                i.WarehousesID == DTO.WarehouseId
                )
                .FirstOrDefaultAsync();

            if (ExistingProducInventory != null)
            {
                ExistingProducInventory.Quantity += DTO.Quantity;
                repository.Update(ExistingProducInventory);
                await repository.SaveAsync();

                DTO.ProductInventoryID = ExistingProducInventory.id;

                return ResponseDTO<AddStockDTO>.Success(DTO, "Updated quantity successfully");
            }
            else
            {
                return ResponseDTO<AddStockDTO>.Error(ErrorCode.NotFound, "Product not found in the selected warehouse. Please add it first");
            }

        }
    }

}
