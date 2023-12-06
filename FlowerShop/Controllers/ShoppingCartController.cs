using FlowerShop.Data;
using FlowerShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlowerShop.Controllers
{
    public class ShoppingCartController : Controller
    {
        //session with the db
        private readonly FlowerShopDBContext _dbContext;

        private readonly OrderInfo _orderInfo;


        public ShoppingCartController(FlowerShopDBContext dBContext)
        {
            _dbContext = dBContext;
            _orderInfo = _dbContext.OrderInfos.SingleOrDefault(o => o.Id == 1);
            
        }

        public ActionResult Index()
        {
            return Content("your current shopping cart");
            //return View(_orderInfo);
        }

        public ActionResult AddToCart(int productId)
        {
            return Content("item was added to the cart");
            ////create a new shopping cart item
            //ShoppingCart item = new();

            ////crate a new new order 
            //OrderInfo orderInfo = new OrderInfo();
            
            ////*****for testing purposes****
            //orderInfo.Id = 1;

            ////pull requested product from the db
            //Product product = _dbContext.Products.SingleOrDefault(p =>p.Id == productId);

            ////validate
            //if(product == null) 
            //{ 
            //    return NotFound();
            //}

            ////update shopping cart item properties
            //item.ProductId = product.Id;
            //item.Quantity += 1;
            //item.Deleted = false;

            //// Implement logic to add items to the order 
            //orderInfo.ShoppingCarts.Add(item);

            //// You may need to check if the item is already in the cart and update the quantity
            //return RedirectToAction("Index");
        }

        public ActionResult RemoveFromCart(int productId)
        {
            // Implement logic to remove items from the cart
            return Content("removed from cart");
        }



    }

}
