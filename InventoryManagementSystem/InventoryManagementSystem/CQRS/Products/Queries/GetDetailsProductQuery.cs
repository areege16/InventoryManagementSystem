
namespace InventoryManagementSystem.CQRS.Products.Queries
{
    public class GetDetailsProductQuery:IRequest<ResponseDTO<ProductDto>>
    {
        public int id { set; get; }
        public GetDetailsProductQuery(int id)
        {
            this.id = id;
        }
    }
    public class GetDetailsProductQueryHandler : IRequestHandler<GetDetailsProductQuery, ResponseDTO<ProductDto>>
    {
        private readonly IGenericRepository<Product> repository;

        public GetDetailsProductQueryHandler(IGenericRepository<Product> repository)
        {
            this.repository = repository;
        }
        public async Task<ResponseDTO<ProductDto>> Handle(GetDetailsProductQuery request, CancellationToken cancellationToken)
        {
            var ProductById =await repository.Get(p => p.id==request.id&&p.IsDeleted==true).FirstOrDefaultAsync();
            if(ProductById==null)
                return ResponseDTO<ProductDto>.Error(ErrorCode.NotFound, "Product Not Found");


            var DetailsProduct = ProductById.Map<ProductDto>();
            return ResponseDTO<ProductDto>.Success(DetailsProduct, "Product retrieved  successfully");

        }
    }

}
