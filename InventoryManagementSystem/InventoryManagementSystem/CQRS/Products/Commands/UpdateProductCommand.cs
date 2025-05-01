using InventoryManagementSystem.Services;

namespace InventoryManagementSystem.CQRS.Products.Commands
{
    public class UpdateProductCommand:IRequest<ResponseDTO<ProductDto>>
    {
        public UpdateProductCommand(UpdateProductDto product)
        {
            Product = product;
        }

        public UpdateProductDto Product { get; set; }
        public int id { get; set; }

    }

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ResponseDTO<ProductDto>>
    {
        private readonly IGenericRepository<Product> repository;
        private readonly IMapper mapper;

        public UpdateProductCommandHandler(IGenericRepository<Product> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<ResponseDTO<ProductDto>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var UpdetedProduct =await repository.GetByIdAsync(request.id);

                if (UpdetedProduct == null)
                    return ResponseDTO<ProductDto>.Error(ErrorCode.NotFound, "Product not found");

                //UpdetedProduct.Map<Product>();
                //mapper.Map(request.Product, UpdetedProduct);
                request.Product.Map(UpdetedProduct);

                repository.Update(UpdetedProduct);
                await repository.SaveAsync();

                var productDto = UpdetedProduct.Map<ProductDto>();

                return ResponseDTO<ProductDto>.Success(productDto, "Product Updated successfully");

            }
            catch (Exception ex)
            {
                return ResponseDTO<ProductDto>.Error(ErrorCode.ServerError, $"Unexpected error: {ex.Message}");
            }
        }
    }
}
