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
            //declare new product object
            Product product = new();

            string standardProductTypeName = HelperMethods.CapitalizeWords(dto.ProductName.Trim());
           
            //get product id
            int productTypeId = HelperMethods.GetProductTypeId(standardProductTypeName);

            //return Content($"productType Id {productTypeId}, productType name {standardProductTypeName}");

            //if the productType is already in the DB
            if (productTypeId > -1) 
            { 
                //assign products type
                product.ProductTypeId = productTypeId;
                product.Name = dto.Name;
                product.Description = dto.Description;
                product.Price = dto.Price;
                product.Deleted = false;
            }

            //if not then create a new productType
            else
            {
                //else create a new product
                ProductType productType = new()
                {
                    Name = dto.ProductName
                };

                //add to db
                _dbContext.ProductTypes.Add(productType);

                //save db
                _dbContext.SaveChanges();

                product.Name = standardProductTypeName;
                product.Description = dto.Description;
                product.ProductTypeId = productType.Id;
                product.Price = dto.Price;
                product.Deleted = false;

            }

            //add to db
            _dbContext.Products.Add(product);

            //save
            _dbContext.SaveChanges();

            //redirect

            return RedirectToAction("Index");
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
