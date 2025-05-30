﻿namespace InventoryManagementSystem.DTO.ProductInventoryDto
{
    public class ProductInventoryProfile:Profile
    {
        public ProductInventoryProfile()
        {
            //CreateMap<ProductInventory, AddInventoryDTO>();

            CreateMap<AddInventoryDTO, ProductInventory>()
            .ForMember(dest => dest.productID, opt => opt.MapFrom(src => src.ProductId))
            .ForMember(dest => dest.WarehousesID, opt => opt.MapFrom(src => src.WarehouseId));

            CreateMap<AddStockDTO, ProductInventory>()
           .ForMember(dest => dest.productID, opt => opt.MapFrom(src => src.ProductId))
           .ForMember(dest => dest.WarehousesID, opt => opt.MapFrom(src => src.WarehouseId));

            CreateMap<TransferStockDTO, ProductInventory>();

            CreateMap<GetLowStockProductDTO, ProductInventory>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.productInventoryID))
                .ReverseMap();

        }
    }
}
