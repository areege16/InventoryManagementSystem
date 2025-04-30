using InventoryManagementSystem.Data;
using InventoryManagementSystem.Interfaces;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IproductRepository
    {
        private readonly InventoryContext context;

        public ProductRepository(InventoryContext context) : base(context)
        {
            this.context = context;
        }

    }
}
