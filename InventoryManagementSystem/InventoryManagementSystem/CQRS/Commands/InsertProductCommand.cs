using InventoryManagementSystem.DTO;
using InventoryManagementSystem.Models;
using MediatR;

namespace InventoryManagementSystem.CQRS.Commands
{
    public record InsertProductCommand(InsertProductDTO Product): IRequest<ResponseDTO<ProductDto>>;
}
