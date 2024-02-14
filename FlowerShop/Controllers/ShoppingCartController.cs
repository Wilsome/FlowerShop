using FlowerShop.Data;
using FlowerShop.Dto;
using FlowerShop.Models;
using FlowerShop.Services;
using FlowerShop.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace FlowerShop.Controllers
{
    public class ShoppingCartController : Controller
    {
        //session with the db
        private readonly FlowerShopDBContext _dbContext;
        private readonly IEmailService _emailService;
        private readonly OrderInfo _orderInfo;
        private decimal _grandTotal = 0.0m;

        public ShoppingCartController(FlowerShopDBContext dBContext, IEmailService emailService)
        {
            _dbContext = dBContext;
            _emailService = emailService;
            _orderInfo = _dbContext.OrderInfos.SingleOrDefault(o => o.Id == 1);
            
        }

        public IActionResult Index()
        {

            List<ShoppingCart> cartItems = _dbContext.ShoppingCarts
                .Include(c => c.Product)
                .ToList();

            foreach (ShoppingCart item in cartItems)
            {
                _grandTotal += item.GetTotal();
            }

            //create a new view model
            CartListWithGrandTotal cartListWithGrandTotal = new()
            {
                ShoppingCarts = cartItems,
                OrderTotal = (_grandTotal * 1.1m)
            };

            return View(cartListWithGrandTotal);
        }

        public IActionResult AddToCart(int productId)
        {
            Product product = _dbContext.Products.Find(productId);

            if (product == null)
            {
                return NotFound();
            }

            OrderInfo currentOrder = GetCurrentOrder();

            ShoppingCart existingCartItem = _dbContext.ShoppingCarts
                .SingleOrDefault(c => c.ProductId == productId && c.OrderInfoId == currentOrder.Id);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity++;
            }
            else
            {
                int shippingAddressId = GetDefaultShippingAddressId(currentOrder.CustomerId);

                ShoppingCart newCartItem = new ShoppingCart
                {
                    OrderInfoId = currentOrder.Id,
                    ProductId = productId,
                    Quantity = 1,
                    Deleted = false,
                    Product = product,
                    OrderInfo = currentOrder
                };

                _dbContext.ShoppingCarts.Add(newCartItem);
            }

            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromCart(int cartItemId)
        {
            ShoppingCart cartItem = _dbContext.ShoppingCarts.Find(cartItemId);

            if (cartItem != null)
            {
                // Decrease the quantity, and remove the item if quantity becomes zero
                if (cartItem.Quantity > 1)
                {
                    cartItem.Quantity--;
                }
                else
                {
                    _dbContext.ShoppingCarts.Remove(cartItem);
                }

                _dbContext.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public IActionResult AddToCartFromReceipt(int receiptItemId)
        {
            Product product = _dbContext.Products.Find(receiptItemId);

            if (product == null)
            {
                return NotFound();
            }

            OrderInfo currentOrder = GetCurrentOrder();

            ShoppingCart existingCartItem = _dbContext.ShoppingCarts
                .SingleOrDefault(c => c.ProductId == receiptItemId && c.OrderInfoId == currentOrder.Id);

            if (existingCartItem == null)
            {
                return NotFound();
            }

            existingCartItem.Quantity++;

            //_dbContext.ShoppingCarts.Add(existingCartItem);

            _dbContext.SaveChanges();

            return RedirectToAction("Index");

        }

        public IActionResult DeleteCart()
        {

            int currentCustomerId = GetCurrentCustomerId();

            // Find all items in the shopping cart associated with the current customer
            List<ShoppingCart> cartItems = _dbContext.ShoppingCarts
                .Where(c => c.OrderInfo.CustomerId == currentCustomerId)
                .ToList();

            // Remove all items from the shopping cart
            _dbContext.ShoppingCarts.RemoveRange(cartItems);
            _dbContext.SaveChanges();

            return RedirectToAction("Index");

        }

        public IActionResult ConfirmOrder() 
        {
            List<ShoppingCart> cartItems = _dbContext.ShoppingCarts
                .Include(c => c.Product)
                .ToList();

            foreach (ShoppingCart item in cartItems)
            {
                _grandTotal += item.GetTotal();
            }

            //create a new view model
            CartListWithGrandTotal cartListWithGrandTotal = new()
            {
                ShoppingCarts = cartItems,
                OrderTotal = (_grandTotal * 1.1m)
            };

            if(cartListWithGrandTotal.ShoppingCarts.Count>0) 
            {
                _emailService.SendEmail(cartListWithGrandTotal);
            }
    
            //clear cart 
            int currentCustomerId = GetCurrentCustomerId();

            // Find all items in the shopping cart associated with the current customer
            List<ShoppingCart> cartItem = _dbContext.ShoppingCarts
                .Where(c => c.OrderInfo.CustomerId == currentCustomerId)
                .ToList();

            if(cartItem.Count>0) 
            {
                // Remove all items from the shopping cart
                _dbContext.ShoppingCarts.RemoveRange(cartItems);
                _dbContext.SaveChanges();

                return RedirectToAction("Index");
            }

            return Json("Your cart is Empty");
            
        }

        private OrderInfo GetCurrentOrder()
        {
            int customerId = GetCurrentCustomerId();

            OrderInfo currentOrder = _dbContext.OrderInfos
                .Include(o => o.OrderState)
                .Include(o => o.ShippingAddress)
                .Include(o => o.Customer)
                .Include(o => o.ShoppingCarts)
                .FirstOrDefault(o => o.CustomerId == customerId && o.OrderState.Name == "Active");

            if (currentOrder == null)
            {
                int shippingAddressId = GetDefaultShippingAddressId(customerId);

                currentOrder = new OrderInfo
                {
                    CustomerId = customerId,
                    Date = DateTime.Now,
                    OrderStateId = GetPendingOrderStateId(),
                    ShippingAddressId = shippingAddressId
                };

                _dbContext.OrderInfos.Add(currentOrder);
                _dbContext.SaveChanges();
            }

            return currentOrder;
        }

        private int GetCurrentCustomerId()
        {
            // Implement this method based on your authentication logic.
            // For example, if you're using ASP.NET Identity, you might retrieve the current user's ID.
            // Alternatively, if you have a custom authentication system, adjust this method accordingly.
            // Example: Replace with actual logic to get the current customer ID.
            return 1;

        }

        private int GetPendingOrderStateId()
        {
            return _dbContext.OrderStates
                .FirstOrDefault(os => os.Name == "Pending")?.Id ?? 0;
        }

        private int GetDefaultShippingAddressId(int customerId)
        {
            return _dbContext.ShippingAddresses
                .Where(sa => sa.CustomerId == customerId && sa.Deleted == false)
                .Select(sa => sa.Id)
                .FirstOrDefault();
        }


    }

}
