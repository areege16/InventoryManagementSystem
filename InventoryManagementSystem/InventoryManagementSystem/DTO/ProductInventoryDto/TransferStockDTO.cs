namespace InventoryManagementSystem.DTO.ProductInventoryDto
{
    public class TransferStockDTO
    {
        public int ProductId { get; set; }
        public int productInventoryID { set; get; }
        public int warehousesIDFrom { get; set; }
        public int warehousesIDTo { get; set; }
        public int Quantity { set; get; }
    }
}
