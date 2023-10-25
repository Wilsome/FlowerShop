using System;
using System.Collections.Generic;

namespace FlowerShop.Models
{
    public partial class ProductType
    {
        public ProductType()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public byte[] Name { get; set; } = null!;

        public virtual ICollection<Product> Products { get; set; }
    }
}
