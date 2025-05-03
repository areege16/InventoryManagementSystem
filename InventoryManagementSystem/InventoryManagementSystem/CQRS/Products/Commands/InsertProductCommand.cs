namespace InventoryManagementSystem.CQRS.Products.Commands
{    public class InsertProductCommand: IRequest<ResponseDTO<ProductDto>>
    {
        public InsertProductDTO Product { get; set; }
        public InsertProductCommand(InsertProductDTO product)
        {
            Product = product;
        }
    }

    public class InsertProductCommandHandler : IRequestHandler<InsertProductCommand, ResponseDTO<ProductDto>>
    {
        private readonly IGenericRepository<Product> repository;

        public InsertProductCommandHandler(IGenericRepository<Product> repository)
        {
            this.repository = repository;
        }

        public async Task<ResponseDTO<ProductDto>>  Handle(InsertProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var product = request.Product.Map<Product>();

                await repository.InsertAsync(product);
                await repository.SaveAsync();

                var productDto = product.Map<ProductDto>();

                return ResponseDTO<ProductDto>.Success(productDto, "Product added successfully");

            }
            catch (Exception ex)
            {
                return ResponseDTO<ProductDto>.Error(ErrorCode.ServerError, $"Unexpected error: {ex.Message}");
            }
        }
    }
    
    
}
