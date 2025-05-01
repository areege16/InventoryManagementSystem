namespace InventoryManagementSystem.DTO.ProductInventoryDto
{
    public class ProductInventoryProfile:Profile
    {
        public ProductInventoryProfile()
        {
            //CreateMap<ProductInventory, AddInventoryDTO>();

            CreateMap<AddInventoryDTO, ProductInventory>()
            .ForMember(dest => dest.productID, opt => opt.MapFrom(src => src.ProductId))
            .ForMember(dest => dest.WarehousesID, opt => opt.MapFrom(src => src.WarehouseId));


        }
    }
}
