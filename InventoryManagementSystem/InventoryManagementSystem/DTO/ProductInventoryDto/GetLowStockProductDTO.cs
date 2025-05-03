namespace InventoryManagementSystem.DTO.ProductInventoryDto
{
    public class GetLowStockProductDTO
    {
        public int productInventoryID { set; get; }
        public int Quantity { set; get; }
        public int LowStockThreshold { set; get; }
        public int productID { set; get; }
    }
}
