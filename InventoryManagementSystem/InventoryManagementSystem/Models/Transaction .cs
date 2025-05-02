using InventoryManagementSystem.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystem.Models
{
    public class Transaction
    {
        public int id { set; get; }
        public TransactionType TransactionType { set; get; }
        public int Quantity { set; get; }
        public DateTime Date { set; get; }


        [ForeignKey("User")]
        public string UserId { set; get; }
        public ApplicationUser User { get; set; }


        [ForeignKey("productInventory")]
        public int productInventoryID { set; get; }
        public ProductInventory productInventory { get; set; }


        [ForeignKey("warehousesFrom")]
        public int? warehousesIDFrom { set; get; }
        public Warehouses warehousesFrom  { get; set; }



        [ForeignKey("warehousesTo")]
        public int warehousesIDTo { set; get; }
        public Warehouses warehousesTo { get; set; }

    }
}
