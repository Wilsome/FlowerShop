using FlowerShop.Data;
using FlowerShop.Dto;
using FlowerShop.Models;
using FlowerShop.Services;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.JSInterop.Infrastructure;

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

        //list of productTypeId
        private IEnumerable<ProductType> _productTypes;

        private List<string> _productTypeNames;

       
        //constructor
        public Admin(FlowerShopDBContext dBContext)
        {
            _dbContext = dBContext;
            _products = dBContext.Products;
            _productTypes = dBContext.ProductTypes;
            _productTypeNames = new List<string>();
            //populate list of product names
            InitProductList();
            
        }
       
        /// <summary>
        /// help method to loop through all the product types and add names to the list
        /// </summary>
        private void InitProductList() 
        {
            foreach (ProductType productType in _productTypes) 
            {
                //only returns a product if it isnt deleted
                _productTypeNames.Add(productType.Name);
                
            }
        }

        //display list of products
        public IActionResult Index()
        {
            //
            return View(_products.Where(p => p.Deleted == false));
        }

        //create a new product
        public IActionResult Create()
        {
            ViewData["ProductNameList"] = _productTypeNames;
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductDto dto) 
        {
            //validate dto

            //get product id
            int productId = HelperMethods.GetProductTypeId(dto.ProductName);

            //if customer wants to enter a new product type
            //need to validate the num is greater than -1/0?
            //create a new product type
            

            //create new product object
            Product product = new()
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                ProductTypeId = productId,
                
            };

            return Content("Created");

            //add to db
            _dbContext.Products.Add(product);

            //save
            _dbContext.SaveChanges();

            //redirect

            return Create();
        }

        //select product to edit
        [HttpGet]
        public IActionResult Update(int productId)
        {
            //generate a list of products names to display in the view 
            ViewData["ProductNameList"] = _productTypeNames;

            //try to pull product from db
            Product product = _products.SingleOrDefault(p => p.Id == productId);
            
            //validate product
            if(product == null) 
            {
                return NotFound();
            }

            return View(product);
        }

        //edit product
        [HttpPost]
        public IActionResult Update(ProductUpdateDto inputProduct) 
        {

            //get product id
            int productTypeId = HelperMethods.GetProductTypeId(inputProduct.ProductTypeName);


            //pull product from DB
            Product product = _products.SingleOrDefault(p => p.Id == inputProduct.Id);
            //validate
            if( product == null) 
            {
                return NotFound();
            }
            //map updated values to product
            product.Name = inputProduct.Name;
            product.Description = inputProduct.Description;
            product.Price = inputProduct.Price;
            product.ProductTypeId = productTypeId;

            //update db
            _dbContext.Update(product);

            //save db
            _dbContext.SaveChanges();

            //redirect
            return RedirectToAction("Index");
        }

        //serve up delete confirmation 
        public IActionResult Delete(int productId) 
        {
            //pull product from the db
            Product product = _products.SingleOrDefault( p => p.Id == productId);

            //validate
            if (product == null) 
            {
                return NotFound();
            }

            return View(product);

        }

        //delete the product
        public IActionResult DeleteProduct(int productId) 
        {
            //pull the product from the db
            Product product = _products.SingleOrDefault(p =>p.Id == productId);
            //validate
            if (product == null) 
            {
                return NotFound();
            }
            
            //update product deleted property
            product.Deleted = true;

            //update db
            _dbContext.Update(product);

            //save db
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }


    }
}
