
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

        #region Insert Inventory
        [HttpPost]
        public async Task<IActionResult> InsertInventory(AddInventoryDTO Inventory)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await mediator.Send(new InsertInventoryCommand(Inventory));
            return Ok(result);
        }
        #endregion

        #region Add Stock
        [HttpPost("AddStock")]
        [Authorize]
        public async Task<IActionResult> AddStock(AddStockDTO addStockDTO)
        {
            var result = await mediator.Send(new AddStockOrchestrator(addStockDTO));
            return Ok(result);
        }
        #endregion

        #region remove Stock
        [HttpPost("removeStock")]
        [Authorize]
        public async Task<IActionResult> removeStock(RemoveStockDTO removeStock)
        {
            var result = await mediator.Send(new RemoveStockOrchestrator(removeStock));
            return Ok(result);
        }
        #endregion

        #region transfer Stock
        [HttpPost("transferStock")]
        [Authorize]
        public async Task<IActionResult> transferStock(TransferStockDTO  transferStockDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await mediator.Send(new TransferStockOrchestrator(transferStockDTO));
            return Ok(result);
        }
        #endregion

        #region Report when quantity lower than lowStock
        [HttpGet("low-stock")]
        public async Task<IActionResult> GetLowStockProducts()
        {
            var result = await mediator.Send(new GetLowStockProductQueries());
            return Ok(result);
        }
        #endregion


    }
}
