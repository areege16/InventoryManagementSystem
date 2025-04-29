using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystem.Models
{
    public class Warehouses
    {
        public int id { get; set; }
        public string Name { set; get; }
        public string Location { set; get; }
        public ICollection<ProductInventory> productInventories { set; get; }


        [InverseProperty("warehousesFrom")]
        public ICollection<Transaction> TransactionsFrom { set; get; }

        [InverseProperty("warehousesTo")]
        public ICollection<Transaction> TransactionsTo { set; get; }

    }
}
