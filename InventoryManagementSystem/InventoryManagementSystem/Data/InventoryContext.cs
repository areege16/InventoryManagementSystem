

namespace InventoryManagementSystem.Data
{
    public class InventoryContext:IdentityDbContext<ApplicationUser> 
    {
        public InventoryContext(DbContextOptions<InventoryContext> options):base(options)
        {
            
        }
        public DbSet<Product>  products { get; set; }
        public DbSet<ProductInventory>  productInventories { get; set; }
        public DbSet<Transaction>  transactions { get; set; }
        public DbSet<Warehouses>  warehouses { get; set; }
        public DbSet<Notification> notifications { get; set; }

        //public DbSet<ApplicationUser> Users { get; set; }

    }
}
