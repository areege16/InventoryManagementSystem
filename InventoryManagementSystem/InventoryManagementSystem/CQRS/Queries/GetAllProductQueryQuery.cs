using InventoryManagementSystem.Models;
using MediatR;

namespace InventoryManagementSystem.CQRS.Queries
{
    public record GetAllProductQueryQuery :IRequest<List<Product>>;
   
}
