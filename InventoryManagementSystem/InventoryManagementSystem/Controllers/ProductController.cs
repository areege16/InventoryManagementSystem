using InventoryManagementSystem.CQRS.Commands;
using InventoryManagementSystem.CQRS.Queries;
using InventoryManagementSystem.Data;
using InventoryManagementSystem.DTO;
using InventoryManagementSystem.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly InventoryContext inventoryContext;
        private readonly IMediator mediator;

        public ProductController(InventoryContext inventoryContext,IMediator mediator)
        {
            this.inventoryContext = inventoryContext;
            this.mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetProduct()
        {
            var result = await mediator.Send(new GetAllProductQuery());
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> InsertProduct(InsertProductDTO product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await mediator.Send(new InsertProductCommand(product));
            return Ok(result);
        }
        //public async Task<ResponseDTO<ProductDto>> InsertProduct(InsertProductDTO product)
        //{
        //    var result = await mediator.Send(new InsertProductCommand(product));
        //    return result;
        //}


    }
}
