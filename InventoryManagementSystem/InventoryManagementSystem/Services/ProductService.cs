using AutoMapper;
using InventoryManagementSystem.DTO;
using InventoryManagementSystem.Enums;
using InventoryManagementSystem.Interfaces;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Services
{
    public class ProductService:IProductService
    {
        private readonly IproductRepository productRepository;
        private readonly IMapper mapper;

        public ProductService(IproductRepository productRepository , IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }
        public async Task<ResponseDTO<ProductDto>> InsertProductAsync(InsertProductDTO dto)
        //public async Task<ProductDto>  InsertProductAsync(InsertProductDTO dto)

        {
            try
            {
                //var product = new Product
                //{
                //    Name = dto.Name,
                //    Price = dto.Price,
                //    Description = dto.Description,
                //    IsDeleted = false
                //};

                var product = mapper.Map<Product>(dto);

                await productRepository.InsertAsync(product);
                await productRepository.SaveAsync();

                //var productDto = new ProductDto
                //{
                //    Id = product.id,
                //    Name = product.Name,
                //    Price = product.Price,
                //    Description = product.Description
                //};

                var productDto = mapper.Map<ProductDto>(product);

                //return productDto;
                return ResponseDTO<ProductDto>.Success(productDto, "Added Successfully");
            }
            catch (Exception ex)
            {
                //return null;
                return ResponseDTO<ProductDto>.Error(ErrorCode.ServerError, $"Unexpected error: {ex.Message}");
            }
        }

        //public async Task<List<ProductDto>> GetAllProductsAsync()
        //{
        //    var products = await productRepository.GetByIdAsync(id);
        //    return products
        //        .Where(p => !p.IsDeleted)
        //        .Select(p => new ProductDto
        //        {
        //            Id = p.id,
        //            Name = p.Name,
        //            Price = p.Price,
        //            Description = p.Description
        //        }).ToList();
        //}

    }
}
