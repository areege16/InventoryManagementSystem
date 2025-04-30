using InventoryManagementSystem.Models;
using MediatR;

namespace InventoryManagementSystem.CQRS.Queries
{
    public record GetAllProductQuery :IRequest<List<Product>>;
   
}
