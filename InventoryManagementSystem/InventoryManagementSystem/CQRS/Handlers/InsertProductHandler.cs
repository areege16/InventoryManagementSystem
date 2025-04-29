using InventoryManagementSystem.CQRS.Commands;
using InventoryManagementSystem.Data;
using InventoryManagementSystem.Models;
using MediatR;

namespace InventoryManagementSystem.CQRS.Handlers
{
    public class InsertProductHandler : IRequestHandler<InsertProductCommand, Product>
    {
        private readonly InventoryContext context;

        public InsertProductHandler(InventoryContext context)
        {
            this.context = context;
        }
        public async Task<Product> Handle(InsertProductCommand request, CancellationToken cancellationToken)
        {
            await context.AddAsync(request.Product);
            context.SaveChanges();
            return await Task.FromResult(request.Product);
        }
    }
}
