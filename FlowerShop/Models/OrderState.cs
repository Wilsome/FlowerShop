using System;
using System.Collections.Generic;

namespace FlowerShop.Models
{
    public partial class OrderState
    {
        public OrderState()
        {
            OrderInfos = new HashSet<OrderInfo>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<OrderInfo> OrderInfos { get; set; }
    }
}
