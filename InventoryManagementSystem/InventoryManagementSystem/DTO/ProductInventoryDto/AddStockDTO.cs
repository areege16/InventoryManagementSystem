namespace InventoryManagementSystem.DTO.ProductInventoryDto
{
    public class AddStockDTO
    {
        public int ProductId { get; set; }
        public int WarehouseId { get; set; }
        public int Quantity { set; get; }

        public int ProductInventoryID { get; set; }
    }
}
