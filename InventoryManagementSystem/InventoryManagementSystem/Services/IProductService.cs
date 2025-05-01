using InventoryManagementSystem.DTO;
using InventoryManagementSystem.DTO.Products;

namespace InventoryManagementSystem.Services
{
    public interface IProductService
    {
        //Task<ResponseDTO<ProductDto>> InsertProductAsync(InsertProductDTO dto);

        Task<ProductDto> InsertProductAsync(InsertProductDTO dto);

        //Task<List<ProductDto>> GetAllProductsAsync();
    }
}
