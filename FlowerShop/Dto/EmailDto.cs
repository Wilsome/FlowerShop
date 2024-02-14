using FlowerShop.Models;
using FlowerShop.ViewModels;

namespace FlowerShop.Dto
{
    public class EmailDto
    {
        public string To { get; set; } = string.Empty;

        public string From { get; set; } = string.Empty;

        public string Date { get; set; } = DateTime.Now.ToShortDateString();

        public string Subject { get; set; } = "Order Confirmation";

        public List<ShoppingCart> ShoppingCarts { get; set; } = new();
        public double Total { get; set; }
    }
}
