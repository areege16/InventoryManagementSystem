namespace InventoryManagementSystem.CQRS.Products.Queries
{
    public class GetAllProductQuery : IRequest<ResponseDTO<List<ProductDto>>>
    {
        public GetAllProductQuery()
        {
        }
    }

    public class GetAllProductQueryHelper : IRequestHandler<GetAllProductQuery, ResponseDTO<List<ProductDto>>>
    {
        private readonly IGenericRepository<Product> repository;
        private readonly IMapper mapper;

        public GetAllProductQueryHelper(IGenericRepository<Product> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<ResponseDTO<List<ProductDto>>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            var products =  repository.Get(p => p.IsDeleted==false);
            var projected = await products
                .ProjectTo<ProductDto>(mapper.ConfigurationProvider)
                .ToListAsync() ;

           return ResponseDTO<List<ProductDto>>.Success(projected, "Products retrieved successfully");
        }

    }
}
