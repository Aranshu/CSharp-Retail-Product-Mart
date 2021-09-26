using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Retail_Product_Management_System.Models;
using Retail_Product_Management_System.Services;
using System.Dynamic;
using Microsoft.AspNetCore.Http;

namespace Retail_Product_Management_System.Controllers
{
    public class CustomerController : Controller
    {
        /*
         * IRepository Object
         */
        private readonly IRepository repositoryObject;

        /*
         * Logging Object
         */
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(CustomerController));

        /*
         * Dependency Injection
         */
        public CustomerController(IRepository _repositoryObject)
        {
            repositoryObject = _repositoryObject;
           _log4net.Info("Logger in CustomerController");
        }

        /*
         * About UsPage
         */
        public IActionResult Index()
        {
            string token = HttpContext.Session.GetString("token");
            if (token == null)
            {
                _log4net.Info("User is not logged in");
                ViewBag.Message = "Please Login First";
                return RedirectToAction("Login", "Home");
            }
            _log4net.Info("Home Page");
            return View();
        }

        /*
         * Product Page Using _ProductCard (Partial View)
         */
        public IActionResult Product()
        {
            List<ProductModel> productListObject = new List<ProductModel>();

            string token = HttpContext.Session.GetString("token");
            if (token == null)
            {
                _log4net.Info("User is not logged in");
                ViewBag.Message = "Please Login First";
                return RedirectToAction("Login", "Home");
            }
            _log4net.Info("User is seeing products");

            try
            {
                productListObject = repositoryObject.GetProducts(token);
            }
            catch (Exception)
            {
                _log4net.Info("Service is down");
            }
            return View(productListObject);
        }

        /*
         * ProductSearch Page
         * Input : Product Id or Name
         * Output : ProductModel
         */
        [HttpGet]
        public IActionResult Search(string productNameOrId)
        {
            string token = HttpContext.Session.GetString("token");

            ProductModel productObject = null;

            if (token == null)
            {
                _log4net.Info("User is not logged in");
                ViewBag.Message = "Please Login First";
                return RedirectToAction("Index", "Login");
            }
            _log4net.Info("User is Searching product by Id");
            
            int number;
            bool success = int.TryParse(productNameOrId, out number);

            if(success)
            {
                if (Int32.Parse(productNameOrId) > 0)
                {
                    productObject = repositoryObject.GetProductsById(token,Int32.Parse(productNameOrId));
                }
                return View("ProductSearch", productObject);
            }
            else
            {
                if (!String.IsNullOrEmpty(productNameOrId))
                {
                    productObject = repositoryObject.GetProductsByName(token,productNameOrId);
                }
                return View("ProductSearch", productObject);
            }
        }

        /*
         * ProductHome Page
         * Input : ProductId
         * Output : Product, ProductModel, VendorModel
         */
        public IActionResult ProductHome(int id)
        {
            dynamic dynamicObject = new ExpandoObject();

            string token = HttpContext.Session.GetString("token");
            if (token == null)
            {
                _log4net.Info("User is not logged in");
                ViewBag.Message = "Please Login First";
                return RedirectToAction("Index", "Login");
            }
            _log4net.Info("product Home Page");

            try
            {
                dynamicObject.ProductList = repositoryObject.GetProducts(token);
                dynamicObject.Product= repositoryObject.GetProductsById(token,id);
                dynamicObject.VendorList = repositoryObject.GetVendorsById(token,id);
            }
            catch (Exception)
            {
                _log4net.Info("Service is down");
            }

            return View(dynamicObject);
        }

        /*
         * Use to add Rating in ProductHome Page
         * Input : ProductId, Ratting
         * Output : Status
         */
        [HttpPost]
        public IActionResult AddRating(int productId, int rate)
        {
            string token = HttpContext.Session.GetString("token");

            if (token == null)
            {
                _log4net.Info("User is not logged in");
                ViewBag.Message = "Please Login First";
                return RedirectToAction("Index", "Login");
            }
            _log4net.Info("User Is Adding Rating");
            var result = repositoryObject.AddRating(token, productId, rate);

            if (result)
            {
                _log4net.Info("updating Rating Success");
                return RedirectToRoute(new { controller = "Customer", action = "ProductHome", id = productId });
            }
            _log4net.Info("updating Rating Failed");
            return BadRequest("Not able to add rating");
        }

        /*
         * Get All Product For Customer Wishlist
         * Ouput : dynamicObject
         */
        public IActionResult Wishlist()
        {
            dynamic dynamicObject = new ExpandoObject();
            string token = HttpContext.Session.GetString("token");
            if (token == null)
            {
                _log4net.Info("User is not logged in");
                ViewBag.Message = "Please Login First";
                return RedirectToAction("Login", "Home");
            }
            int customerid = (int)HttpContext.Session.GetInt32("customerId");
            _log4net.Info("User is seeing WishList");
            try
            {
                dynamicObject.customerWishlist = repositoryObject.GetWishlists(token, customerid);
                dynamicObject.ProductList = repositoryObject.GetProducts(token);
            }
            catch (Exception)
            {
                _log4net.Info("Service is down");
            }
            return View(dynamicObject);
        }

        /*
         * Adding Product To WishList
         * Input : ProductId
         * Output : Status
         */
        public IActionResult UpdateWishlist(int Id)
        {
            string token = HttpContext.Session.GetString("token");
            if (token == null)
            {
                _log4net.Info("User is not logged in");
                ViewBag.Message = "Please Login First";
                return RedirectToAction("Login", "Home");
            }
            int customerid = (int)HttpContext.Session.GetInt32("customerId");
            _log4net.Info("User is Adding Item Into WishList");
            var result = repositoryObject.PostWishlists(token, Id, customerid);
            if (result)
            {
                _log4net.Info("Adding Product to Wishlist Success");
                return RedirectToRoute(new { controller = "Customer", action = "Wishlist" });
            }
            _log4net.Info("Adding Product to Wishlist Failed");
            return RedirectToRoute(new { controller = "Customer", action = "Index" });
        }

        /*
         * Get Cart Detail
         * Output : Dynamic Object
         */
        public IActionResult Cart()
        {
            dynamic dynamicObject = new ExpandoObject();
            string token = HttpContext.Session.GetString("token");
            if (token == null)
            {
                _log4net.Info("User is not logged in");
                ViewBag.Message = "Please Login First";
                return RedirectToAction("Login", "Home");
            }
            int customerid = (int)HttpContext.Session.GetInt32("customerId");
            _log4net.Info("User is Adding Item Into WishList");
            try
            {
                dynamicObject.CartList = repositoryObject.GetCartDetail(token, customerid);
                dynamicObject.ProductList = repositoryObject.GetProducts(token);
                dynamicObject.VendorList = repositoryObject.GetVendors(token);
            }
            catch (Exception)
            {
                _log4net.Info("Service is down");
            }
            return View(dynamicObject);
        }

        /*
         * Add Product To Cart
         * Input : VendorId, ProductId, Day, zipcode
         * Output : Status
         */
        public IActionResult AddToCart(int venderId, int productId, int Day, int zipcode)
        {
            string token = HttpContext.Session.GetString("token");
            if (token == null)
            {
                _log4net.Info("User is not logged in");
                ViewBag.Message = "Please Login First";
                return RedirectToAction("Login", "Home");
            }
            int customerid = (int)HttpContext.Session.GetInt32("customerId");
            _log4net.Info("User is Adding Product to Cart");
            var resultupdate = repositoryObject.UpdateQuantity(token, productId, venderId);
            CartModel cart = new CartModel();
            cart.CustomerId = customerid;
            cart.VendorId = venderId;
            cart.ProductId = productId;
            cart.DeliveryDate = DateTime.Now.AddDays(Day);
            cart.Zipcode = zipcode;
            var resultcart = repositoryObject.AddCart(token, cart);
            if (resultupdate && resultcart)
            {
                return RedirectToRoute(new { controller = "Customer", action = "Cart"});
            }
            return RedirectToRoute(new { controller = "Customer", action = "Index" });
        }

        /*
         * Delete a Product From Cart
         * Input : CartId
         * Output : Redirect
         */
        public IActionResult DeleteCart(int CartId)
        {
            string token = HttpContext.Session.GetString("token");
            if (token == null)
            {
                _log4net.Info("User is not logged in");
                ViewBag.Message = "Please Login First";
                return RedirectToAction("Login", "Home");
            }
            _log4net.Info("User is Deleting Product in Cart");
            repositoryObject.DeleteCartProduct(token, CartId);
            return RedirectToRoute(new { controller = "Customer", action = "Cart" });
        }

        /*
         * Delete All Product From Cart
         * Output : Redirect
         */
        public IActionResult DeleteAllCart()
        {
            string token = HttpContext.Session.GetString("token");
            if (token == null)
            {
                _log4net.Info("User is not logged in");
                ViewBag.Message = "Please Login First";
                return RedirectToAction("Login", "Home");
            }
            int customerid = (int)HttpContext.Session.GetInt32("customerId");
            _log4net.Info("User is Has Deleted Cart");
            repositoryObject.Checkout(token, customerid);
            return RedirectToRoute(new { controller = "Customer", action = "Cart" });
        }

        /*
         * Delete All Product From Cart
         * Output : View
         */
        public IActionResult Checkout()
        {
            string token = HttpContext.Session.GetString("token");
            if (token == null)
            {
                _log4net.Info("User is not logged in");
                ViewBag.Message = "Please Login First";
                return RedirectToAction("Login", "Home");
            }
            int customerid = (int)HttpContext.Session.GetInt32("customerId");
            _log4net.Info("User is Has Deleted Cart");
            repositoryObject.Checkout(token, customerid);
            return View();
        }
    }
}
