namespace InventoryManagementSystem.DTO.ProductInventoryDto
{
    public class AddInventoryDTO
    {
        public int ProductId { get; set; }
        public int WarehouseId { get; set; }
        public int Quantity { set; get; }
        public int LowStockThreshold { set; get; }
    }
}
