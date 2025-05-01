using System.Collections.ObjectModel;

namespace InventoryManagementSystem.Models
{
    public class Product
    {
        public int id { set; get; }
        public string Name { set; get; }
        public double Price { set; get; }
        public string? Description { set; get; }
        public bool IsDeleted { set; get; } = false;

        public ICollection<ProductInventory> productInventories { set; get; }


    }
}
