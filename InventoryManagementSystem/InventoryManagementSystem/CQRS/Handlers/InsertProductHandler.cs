using InventoryManagementSystem.CQRS.Commands;
using InventoryManagementSystem.DTO;
using InventoryManagementSystem.Interfaces;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Services;
using MediatR;

namespace InventoryManagementSystem.CQRS.Handlers
{
    public class InsertProductHandler : IRequestHandler<InsertProductCommand, ResponseDTO<ProductDto>>
    {


        private readonly IProductService productService;

        public InsertProductHandler(IProductService productService)
        {
            this.productService = productService;
        }
        public async Task<ResponseDTO<ProductDto>> Handle(InsertProductCommand request, CancellationToken cancellationToken)
        {
            return await productService.InsertProductAsync(request.Product);

        }
    }
}
