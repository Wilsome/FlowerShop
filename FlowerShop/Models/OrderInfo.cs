using System;
using System.Collections.Generic;

namespace FlowerShop.Models
{
    public partial class OrderInfo
    {
        public OrderInfo()
        {
            ShoppingCarts = new HashSet<ShoppingCart>();
        }

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ShippingAddressId { get; set; }
        public int OrderStateId { get; set; }
        public DateTime Date { get; set; }
        public string? Notes { get; set; }
        public bool Deleted { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        public virtual OrderState OrderState { get; set; } = null!;
        public virtual ShippingAddress ShippingAddress { get; set; } = null!;
        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }
    }
}
