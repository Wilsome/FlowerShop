    namespace FlowerShop.Dto
{
    public class ProductDto
    {
        //public int ProductTypeId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Image { get; set; } = string.Empty!;
        public decimal Price { get; set; }
        public string ProductName { get; set; } = string.Empty;

    }
}
