namespace InventoryManagementSystem.DTO.Products
{
    public class UpdateProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string? Description { get; set; }
        public bool isDeleted { get; set; }
    }
}
