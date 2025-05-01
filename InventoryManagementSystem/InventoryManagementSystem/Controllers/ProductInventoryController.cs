using InventoryManagementSystem.CQRS.InventoryProduct.Commands;
using InventoryManagementSystem.DTO.ProductInventoryDto;
using InventoryManagementSystem.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{

   
    [Route("api/[controller]")]
    [ApiController]

    public class ProductInventoryController : ControllerBase
    {
        private readonly InventoryContext inventoryContext;
        private readonly IMediator mediator;

        public ProductInventoryController(InventoryContext inventoryContext, IMediator mediator)
        {
            this.inventoryContext = inventoryContext;
            this.mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> InsertInventory(AddInventoryDTO Inventory)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await mediator.Send(new InsertInventoryCommand(Inventory));
            return Ok(result);
        }
    }
}
