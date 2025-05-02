
namespace InventoryManagementSystem.DTO.TrasactionsDTO
{
    public class AddTransactionDto
    {
        public TransactionType TransactionType { set; get; }
        public int Quantity { set; get; }
        public DateTime Date { set; get; } = DateTime.UtcNow;

        public string UserId { set; get; }

        public int ProductInventoryID { set; get; }

        public int warehousesIDTo { set; get; }
      

    }
}
