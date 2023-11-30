using System;
using System.Collections.Generic;

namespace FlowerShop.Models
{
    public partial class Product
    {
        public Product()
        {
            ShoppingCarts = new HashSet<ShoppingCart>();
        }

        public int Id { get; set; }
        public int ProductTypeId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public bool Deleted { get; set; }

        //public string? Image { get; set; } = string.Empty;

        public virtual ProductType ProductType { get; set; } = null!;
        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }
    }
}
