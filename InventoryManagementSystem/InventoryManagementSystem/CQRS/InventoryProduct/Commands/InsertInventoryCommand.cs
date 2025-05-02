
using InventoryManagementSystem.DTO.ProductInventoryDto;
using InventoryManagementSystem.DTO.Products;

namespace InventoryManagementSystem.CQRS.InventoryProduct.Commands
{
    public class InsertInventoryCommand:IRequest<ResponseDTO<AddInventoryDTO>>
    {
       public AddInventoryDTO inventoryDTO { set; get; }
        public InsertInventoryCommand(AddInventoryDTO inventoryDTO)
        {
            this.inventoryDTO = inventoryDTO;
        }
    }

    public class InsertInventoryCommandHandler : IRequestHandler<InsertInventoryCommand,ResponseDTO<AddInventoryDTO>>
    {
        private readonly IGenericRepository<ProductInventory> repository;
        private readonly IMapper mapper;

        public InsertInventoryCommandHandler(IGenericRepository<ProductInventory> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<ResponseDTO<AddInventoryDTO>> Handle(InsertInventoryCommand request, CancellationToken cancellationToken)
        {
            var DTO = request.inventoryDTO; 
            //Check if Product Exist in Inventory
            var ExistingProducInventory = repository.Get(
                i => i.productID == DTO.ProductId &&
                i.WarehousesID == DTO.WarehouseId
                )
                .FirstOrDefault();

            if (ExistingProducInventory != null)
            {
                return ResponseDTO<AddInventoryDTO>.Error(ErrorCode.Conflict, "This product already exists in the inventory for the selected warehouse");
            }
            else
            {
                var NewInventory = DTO.Map<ProductInventory>();

                await repository.InsertAsync(NewInventory);
                await repository.SaveAsync();
            }
            return ResponseDTO<AddInventoryDTO>.Success(DTO, "Product added successfully");
        }
    }
}
