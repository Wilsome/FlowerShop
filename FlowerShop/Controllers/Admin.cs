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
    [Authorize]
    public class Admin : Controller
    {
        //session witht the flower shop db
        private readonly FlowerShopDBContext _dbContext;

        //list of products
        private IEnumerable<Product> _products;

        //list of productType objects
        private IEnumerable<ProductType> _productTypes;

        //list of productType names
        private List<string> _productTypeNames;


        //constructor
        public Admin(FlowerShopDBContext dBContext)
        {
            _dbContext = dBContext;
            _products = dBContext.Products;
            _productTypes = dBContext.ProductTypes;
            _productTypeNames = _productTypes.Select(p => p.Name).ToList();
        }

        //display list of products
        public IActionResult Index()
        {
            //simulated delete. Will only return the products where deleted isnt set to false. 
            return View(_products.Where(p => p.Deleted == false));
        }

        //create a new product
        public IActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductDto dto)
        {
            //declare new product object
            Product product = new();

            string standardProductTypeName = HelperMethods.CapitalizeWords(dto.ProductName.Trim());

            //get product id
            int productTypeId = GetProductTypeId(standardProductTypeName);

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

            else 
            {
                throw new Exception("ProductTypeId not valid");
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
            if (product == null)
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
            int productTypeId = GetProductTypeId(inputProduct.ProductTypeName);


            //pull product from DB
            Product product = _products.SingleOrDefault(p => p.Id == inputProduct.Id);
            //validate
            if (product == null)
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
            Product product = _products.SingleOrDefault(p => p.Id == productId);

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
            Product product = _products.SingleOrDefault(p => p.Id == productId);
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

        /// <summary>
        /// Gets the productTypeId from productType Name
        /// </summary>
        /// <param name="productTypeName"></param>
        /// <returns></returns>
        private int GetProductTypeId(string productTypeName)
        {
            int productTypeId = -1;

            //loop through the list of typenames
            for (int i = 0; i < _productTypeNames.Count; i++)
            {
                if (productTypeName == _productTypeNames[i])
                {
                    //get the productType object
                    //if multiple product types get entered///////////
                    ProductType proType = _productTypes.FirstOrDefault(p => p.Name == productTypeName);

                    //update typeId from the productType object Id
                    productTypeId = proType.Id;

                    return productTypeId;

                }

            }

            //if not found create new productType object
            ProductType productType = new()
            {
                Name = productTypeName
            };

            //add to db
            _dbContext.ProductTypes.Add(productType);

            //save db
            _dbContext.SaveChanges();

            //update productTypeId based off the new save
            productTypeId = productType.Id;

            return productTypeId;

        }

    }
}
