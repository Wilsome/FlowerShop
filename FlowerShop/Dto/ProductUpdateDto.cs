namespace FlowerShop.Dto
{
    public class ProductUpdateDto
    {
        public int Id { get; set; }
        public string ProductTypeName { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public bool Deleted { get; set; }
    }
}
