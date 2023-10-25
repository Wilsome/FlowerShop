using System;
using System.Collections.Generic;

namespace FlowerShop.Models
{
    public partial class ShoppingCart
    {
        public int Id { get; set; }
        public int OrderInfoId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public bool Deleted { get; set; }

        public virtual OrderInfo OrderInfo { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
