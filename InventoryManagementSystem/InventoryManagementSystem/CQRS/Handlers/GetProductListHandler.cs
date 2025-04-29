//using InventoryManagementSystem.CQRS.Queries;
//using InventoryManagementSystem.Data;
//using InventoryManagementSystem.Models;
//using MediatR;

//namespace InventoryManagementSystem.CQRS.Handlers
//{
//    public class GetProductListHandler : IRequestHandler<GetAllProductQueryQuery, List<Product>>
//    {
//        private readonly InventoryContext context;

//        public GetProductListHandler(InventoryContext context)
//        {
//            this.context = context;
//        }
//        public async Task<List<Product>> Handle(GetAllProductQueryQuery request, CancellationToken cancellationToken)
//        {
//            return await Task.FromResult(context.)

//        }  
//    }
//}
