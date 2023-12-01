using FlowerShop.Data;
using FlowerShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlowerShop.Controllers
{
    public class ShoppingCartController : Controller
    {
        //session with the db
        private readonly FlowerShopDBContext _dbContext;

        //list of products
        private IEnumerable<Product> _products;

        //list of shopping cart items
        private readonly List<ShoppingCart> cart = new();

        public ShoppingCartController(FlowerShopDBContext dBContext)
        {
            _dbContext = dBContext;
            _products = _dbContext.Products;

        }

        public ActionResult Index()
        {
            //return View(cart);
            return Content("Your shopping cart");
        }

        public ActionResult AddToCart(int productId)
        {
            //create a new shopping cart item
            ShoppingCart item = new();

            //pull requested product from the db
            Product product = _dbContext.Products.SingleOrDefault(p =>p.Id == productId);

            //validate
            if(product == null) 
            { 
                return NotFound();
            }

            //update shopping cart item properties
            item.ProductId = product.Id;
            item.Quantity += 1;
            item.Deleted = false;

            // Implement logic to add items to the cart
            // You may need to check if the item is already in the cart and update the quantity
            return Json(item);
        }

        public ActionResult RemoveFromCart(int productId)
        {
            // Implement logic to remove items from the cart
            return Content("removed from cart");
        }
    }

}
