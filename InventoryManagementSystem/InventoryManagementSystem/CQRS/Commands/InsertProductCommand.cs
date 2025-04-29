using InventoryManagementSystem.Models;
using MediatR;

namespace InventoryManagementSystem.CQRS.Commands
{
    public record InsertProductCommand(Product Product): IRequest<Product>;
   
}
