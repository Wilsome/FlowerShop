using FlowerShop.Data;
using FlowerShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlowerShop.Controllers
{
    //Hide admin features behind authorization check 
    //[Authorize]
    public class Admin : Controller
    {
        //session witht the flower shop db
        private readonly FlowerShopDBContext _dbContext;

        //list of products
        private IEnumerable<Product> _products;

        //constructor
        public Admin(FlowerShopDBContext dBContext)
        {
            _dbContext = dBContext;
            _products = dBContext.Products;
        }

        //display list of products
        public IActionResult Index()
        {
            return View(_products);
        }

        //create a new product
        public IActionResult Create(Exception ex)
        {
            throw new NotImplementedException($"{ex}, not implemented");
        }

        //edit a product
        public IActionResult Update(Product product)
        {
            //try to pull product from db
            Product product1 = _products.SingleOrDefault(p => p.Id == product.Id);
            
            //validate product
            if(product == null) 
            {
                return NotFound();
            }

            return View(product1);
        }

        //delete a product  
        public IActionResult Delete(Exception ex) 
        {
            throw new NotImplementedException($"{ex}, not implemented");
        }


    }
}
