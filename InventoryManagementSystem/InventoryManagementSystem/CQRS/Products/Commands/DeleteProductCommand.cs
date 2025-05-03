using InventoryManagementSystem.DTO.Products;

namespace InventoryManagementSystem.CQRS.Products.Commands
{
    public class DeleteProductCommand:IRequest
    {
        public int id { get; set; }
        public DeleteProductCommand(int id)
        {
            this.id = id;

        }
    }
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IGenericRepository<Product> repository;

        public DeleteProductCommandHandler(IGenericRepository<Product> repository )
        {
            this.repository = repository;
        }
        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = repository.Get(p => p.id == request.id).FirstOrDefault();
            if(product==null)
                throw new KeyNotFoundException("Product not found");

            product.IsDeleted = true;
            repository.Update(product);
            await repository.SaveAsync();

            return Unit.Value;

        }
    }
}
