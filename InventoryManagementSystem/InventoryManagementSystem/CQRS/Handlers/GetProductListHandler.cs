using InventoryManagementSystem.CQRS.Queries;
using InventoryManagementSystem.Data;
using InventoryManagementSystem.Interfaces;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories;
using MediatR;

namespace InventoryManagementSystem.CQRS.Handlers
{
    public class GetProductListHandler : IRequestHandler<GetAllProductQuery, List<Product>>
    {
        private readonly IproductRepository productRepository;

        public GetProductListHandler(IproductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public async Task<List<Product>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            //return await Task.FromResult(productRepository.GetAll().ToList());
            return  productRepository.GetAll().ToList();
        }  
    }
}
