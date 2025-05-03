namespace InventoryManagementSystem.Models
{
    public class Notification
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;


        [ForeignKey("productInventory")]
        public int productInventoryID { set; get; }
        public ProductInventory productInventory { get; set; }

    }
}
