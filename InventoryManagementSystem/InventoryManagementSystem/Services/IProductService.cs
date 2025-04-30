using InventoryManagementSystem.DTO;

namespace InventoryManagementSystem.Services
{
    public interface IProductService
    {
        Task<ResponseDTO<ProductDto>> InsertProductAsync(InsertProductDTO dto);

        //Task<List<ProductDto>> GetAllProductsAsync();
    }
}
