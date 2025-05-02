using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystem.DTO.TrasactionsDTO
{
    public class TransferTransactionDto
    {
        public TransactionType TransactionType { set; get; }
        public int Quantity { set; get; }
        public DateTime Date { set; get; }

        public string UserId { set; get; }

        public int productInventoryID { set; get; }

        public int warehousesIDFrom { set; get; }

        public int warehousesIDTo { set; get; }

    }
}
