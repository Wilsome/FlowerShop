using FlowerShop.Data;
using FlowerShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace FlowerShop.Controllers
{
    [Authorize]
    public class ShopController: Controller
    {
        private readonly FlowerShopDBContext _dbContext;

        private IEnumerable<Product> _products;

        public ShopController(FlowerShopDBContext dBContext)
        {
            _dbContext = dBContext;
            _products = _dbContext.Products;
            
        }

        //display a list of products
        public IActionResult Index() 
        {
            //only returns a product if it isnt deleted
            return View(_products.Where(p => p.Deleted == false));
        }

        //display a single product
        public IActionResult Details(int id) 
        {
            Console.WriteLine(id.ToString());
            //create a product object
            Product product = null;

            //validatd id
            if (id >= 0 ) 
            { 
                product = _products.SingleOrDefault(p => p.Id == id);
            }

            //check product for null value
            if(product == null) 
            { 
                return NotFound();
            }

            return View(product);
        }

    }
}
