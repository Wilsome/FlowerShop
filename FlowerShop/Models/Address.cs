using System;
using System.Collections.Generic;

namespace FlowerShop.Models
{
    public partial class Address
    {
        public Address()
        {
            ShippingAddresses = new HashSet<ShippingAddress>();
        }

        public int Id { get; set; }
        public string HouseNumber { get; set; } = null!;
        public string StreetName { get; set; } = null!;
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public string Zip { get; set; } = null!;
        public string? AptNumber { get; set; }
        public string? AptName { get; set; }
        public bool? Deleted { get; set; }

        public virtual ICollection<ShippingAddress> ShippingAddresses { get; set; }
    }
}
