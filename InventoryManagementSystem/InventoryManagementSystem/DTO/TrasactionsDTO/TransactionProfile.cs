using System.Security.Cryptography;

namespace InventoryManagementSystem.DTO.TrasactionsDTO
{
    public class TransactionProfile:Profile
    {
        public TransactionProfile()
        {
            CreateMap<AddTransactionDto, Transaction>().ReverseMap();
            CreateMap<TransferTransactionDto, Transaction>().ReverseMap();


            //    .ForMember(dst=>dst.warehousesFrom,
            //    opt=>opt.MapFrom(src=>src.WarehousesID));

            //CreateMap<Transaction, AddTransactionDto>()
            //  .ForMember(dst => dst.WarehousesID,
            //  opt => opt.MapFrom(src => src.warehousesFrom));

        }
    }
}
