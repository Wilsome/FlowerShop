using System;
using System.Collections.Generic;

namespace FlowerShop.Models
{
    public partial class ShippingAddress
    {
        public ShippingAddress()
        {
            OrderInfos = new HashSet<OrderInfo>();
        }

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int AddressId { get; set; }
        public bool Deleted { get; set; }

        public virtual Address Address { get; set; } = null!;
        public virtual Customer Customer { get; set; } = null!;
        public virtual ICollection<OrderInfo> OrderInfos { get; set; }
    }
}
