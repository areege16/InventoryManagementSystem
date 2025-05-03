
using InventoryManagementSystem.CQRS.Products.Queries;

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
      
        #region Add New Product
        [HttpPost] //api/Product
        public async Task<IActionResult> InsertProduct(InsertProductDTO product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await mediator.Send(new InsertProductCommand(product));
            return Ok(result);
        }
        #endregion

        #region Update Product

        [HttpPut("{id:int}")] //api/Product
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductDto product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await mediator.Send(new UpdateProductCommand(product) { id = id});
            return Ok(result);
        }
        #endregion

        #region Soft Delete Product

        [HttpDelete("{id:int}")] //api/Product
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await mediator.Send(new DeleteProductCommand(id));
            return NoContent();
        }
        #endregion

        #region Get Details Product

        [HttpGet("GetProductDetails/{id:int}")] //api/Product/GetProductDetails/{id}
        public async Task<IActionResult> GetProductDetails(int id)
        {
            var result = await mediator.Send(new GetDetailsProductQuery(id));
            return Ok(result);
        }
        #endregion

        #region Get All Products

        [HttpGet("GetAllProducts")] //api/Product/GetAllProducts
        public async Task<IActionResult> GetAllProducts()
        {
            var result = await mediator.Send(new GetAllProductQuery());
            return Ok(result);
        }
        #endregion

        #region Get With Product Quantity 

        [HttpGet("GetWithProductQuantity")] 
        public async Task<IActionResult> GetWithProductQuantity()
        {
            var result = await mediator.Send(new GetProductsWithQuantityQuery());
            return Ok(result);
        }
        #endregion
    }
}
