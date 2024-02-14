using FlowerShop.Models;

namespace FlowerShop.ViewModels
{
    public class CartListWithGrandTotal
    {
        public List<ShoppingCart> ShoppingCarts { get; set; } = new List<ShoppingCart>();

        public decimal OrderTotal { get; set; } = 0;
    }
}
