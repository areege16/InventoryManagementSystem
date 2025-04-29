using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystem.Models
{
    public class ProductInventory
    {
        public int id { set; get; }
        public int Quantity { set; get; }
        public int LowStockThreshold { set; get; }

        [ForeignKey("product")]
        public int productID { set; get; }
        public Product product { get; set; }


        [ForeignKey("warehouses")]
        public int WarehousesID { set; get; }
        public Warehouses warehouses { get; set; }

        public ICollection<Transaction> Transactions { get; set; }

    }
}
