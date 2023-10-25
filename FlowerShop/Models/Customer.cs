using System;
using System.Collections.Generic;

namespace FlowerShop.Models
{
    public partial class Customer
    {
        public Customer()
        {
            OrderInfos = new HashSet<OrderInfo>();
            ShippingAddresses = new HashSet<ShippingAddress>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string Email { get; set; } = null!;
        public bool Deleted { get; set; }

        public virtual ICollection<OrderInfo> OrderInfos { get; set; }
        public virtual ICollection<ShippingAddress> ShippingAddresses { get; set; }
    }
}
