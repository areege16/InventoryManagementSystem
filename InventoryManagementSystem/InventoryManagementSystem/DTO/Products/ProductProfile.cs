
namespace InventoryManagementSystem.DTO.Products
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            //source.Map<TDestination>()
           // CreateMap<From, To>();

            CreateMap<Product, ProductDto>();
            CreateMap<InsertProductDTO, Product>();

            CreateMap<Product, UpdateProductDto>();
            CreateMap<UpdateProductDto, Product>()
                .ForMember(dst=>dst.id,opt=>opt.Ignore());

            CreateMap<Product, GetProductWithQuentityDTO>()
                .ForMember(dst => dst.Quantity, opt => opt.MapFrom(src =>
                src.productInventories.FirstOrDefault()!=null?
                src.productInventories.FirstOrDefault().Quantity
                :0));
                


        }
    }
}
