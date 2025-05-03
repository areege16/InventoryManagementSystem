namespace InventoryManagementSystem.CQRS.Products.Queries
{
    public class GetProductsWithQuantityQuery:IRequest<ResponseDTO<List<GetProductWithQuentityDTO>>>
    {
        
    }
    public class GetProductsWithQuantityQueryHandler : IRequestHandler<GetProductsWithQuantityQuery, ResponseDTO<List<GetProductWithQuentityDTO>>>
    {
        private readonly IGenericRepository<Product> repository;
        private readonly IMapper mapper;

        public GetProductsWithQuantityQueryHandler(IGenericRepository<Product> repository , IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<ResponseDTO<List<GetProductWithQuentityDTO>>> Handle(GetProductsWithQuantityQuery request, CancellationToken cancellationToken)
        {
            var productsWithInventory =await repository
                     .Get(p => p.IsDeleted == false)
                     .Include(p => p.productInventories)
                     .ToListAsync(cancellationToken);

            var result = mapper.Map<List<GetProductWithQuentityDTO>>(productsWithInventory);

            return ResponseDTO<List<GetProductWithQuentityDTO>>.Success(result, "Products retrieved successfully");

        }
    }


}
