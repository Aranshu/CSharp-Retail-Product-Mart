using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Retail_Product_Management_System.Models;

namespace Retail_Product_Management_System.Services
{
    public class Repository : IRepository
    {
        /*
         * Configration Object
         */
        private readonly IConfiguration _configuration;
       
        /*
         * Logging Object
         */
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger("log");

        /*
         * Variable declaration
         */
        private readonly string productURI;
        private readonly string proceedToBuyUri;
        private readonly string vendorUri;

        /*
         * Dependency Injection
         */
        public Repository(IConfiguration configuration)
        {
            _configuration = configuration;
            productURI = _configuration.GetValue<string>("MyLinkValue:ProductUri");
            proceedToBuyUri = _configuration.GetValue<string>("MyLinkValue:ProceedToBuyUri");
            vendorUri = _configuration.GetValue<string>("MyLinkValue:VendorUri");

        }

        /*
         * Fuction to extract all Products
         * Input : JWT Token
         * Output : Product Model
         */
        public List<ProductModel> GetProducts(string token)
        {
            _log4net.Info("fetching products from product microservice");

            List<ProductModel> products = null;

            string bearer = String.Format("Bearer {0}",token);

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(productURI);
                try
                {
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Add("Authorization",bearer);

                    var response = httpClient.GetAsync("api/Product/GetAllProducts");

                    response.Wait();
                    var result = response.Result;

                    if (!result.IsSuccessStatusCode)
                    {
                        _log4net.Info("Product microservice failed");

                        return null;
                    }
                    var ApiResponse = result.Content.ReadAsAsync<List<ProductModel>>();
                    ApiResponse.Wait();

                    products = ApiResponse.Result;

                }
                catch (Exception)
                {
                    _log4net.Error("Product Microservice is Down!!");
                }
            }

            return products;
        }

        /*
         * Function To Search Product With Help of ID
         * Input : JWT Token, ProductId 
         * Output : Product
         */
        public ProductModel GetProductsById(string token, int id)
        {
            _log4net.Info("fetching products by ID from product microservice");

            ProductModel productsById = null;

            string bearer = String.Format("Bearer {0}", token);

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(productURI);
                try
                {
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Add("Authorization", bearer);

                    var response = httpClient.GetAsync("api/Product/SearchProductById/" + id);

                    response.Wait();
                    var result = response.Result;

                    if (!result.IsSuccessStatusCode)
                    {
                        _log4net.Info("Product microservice failed");

                        return null;
                    }
                    var ApiResponse = result.Content.ReadAsAsync<ProductModel>();
                    ApiResponse.Wait();

                    productsById = ApiResponse.Result;

                }
                catch (Exception)
                {
                    _log4net.Error("Product Microservice is Down!!");
                }
            }
            return productsById;
        }

        /*
         * Function To Search Product With Help of Name
         * Input : JWT Token, ProductName 
         * Output : Product
         */
        public ProductModel GetProductsByName(string token, string name)
        {
            _log4net.Info("fetching products by name from product microservice");

            ProductModel productsByName = null;

            string bearer = String.Format("Bearer {0}", token);

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(productURI);
                try
                {
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    //httpClient.DefaultRequestHeaders.Add("Authorization", bearer);

                    var response = httpClient.GetAsync("api/Product/GetProductByName/" + name);

                    response.Wait();
                    var result = response.Result;

                    if (!result.IsSuccessStatusCode)
                    {
                        _log4net.Info("Product microservice failed");

                        return null;
                    }
                    var ApiResponse = result.Content.ReadAsAsync<ProductModel>();
                    ApiResponse.Wait();

                    productsByName = ApiResponse.Result;
                }
                catch (Exception)
                {
                    _log4net.Error("Product Microservice is Down!!");
                }
            }

            return productsByName;
        }

        /*
         * Add Rating to a product
         * Input : JWT Token, ProductId, ProductRating
         * Output : True or False
         */
        public Boolean AddRating(string token, int id, int rating)
        {
            _log4net.Info("Adding rating");

            string bearer = String.Format("Bearer {0}", token);

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(productURI);
                try
                {
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Add("Authorization", bearer);
                    var pocoObject = new
                    {
                        Id = id,
                        rating = rating
                    };

                    var response = httpClient.PostAsJsonAsync("api/Product/AddProductRating", pocoObject);

                    response.Wait();
                    var result = response.Result;

                    if (!result.IsSuccessStatusCode)
                    {
                        _log4net.Info("Product microservice failed");
                        return false;
                    }

                }
                catch (Exception)
                {
                    _log4net.Error("Product Microservice is Down!!");
                }
                return true;
            }
        }

        /*
         * Get The WishList Of Customer
         * Input : JWT Token, CustomerId
         * Output : CustomerWishlistModel
         */
        public List<CustomerWishlistModel> GetWishlists(string token, int id)
        {
            _log4net.Info("Get WishList");
            string bearer = String.Format("Bearer {0}", token);
            List<CustomerWishlistModel> wishList = null;
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(proceedToBuyUri);
                try
                {
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Add("Authorization", bearer);

                    var response = httpClient.GetAsync("api/ProceedToBuy/GetWishList/" + id);

                    response.Wait();
                    var result = response.Result;

                    if (!result.IsSuccessStatusCode)
                    {
                        _log4net.Info("Proceed to buy microservice in wishlist failed");

                        return null;
                    }
                    var ApiResponse = result.Content.ReadAsAsync<List<CustomerWishlistModel>>();
                    ApiResponse.Wait();
                    wishList = ApiResponse.Result;

                }
                catch (Exception)
                {
                    _log4net.Error("proceed to buy Microservice is Down!!");
                }

            }
            return wishList;
        }

        /*
         * Add Product to WishList
         * Input : JWT Token, ProductId, CustomerId
         * Output : Status
         */
        public Boolean PostWishlists(string token, int productId, int customerId)
        {
            _log4net.Info("Post WishList");
            string bearer = String.Format("Bearer {0}", token);

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(proceedToBuyUri);
                try
                {
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Add("Authorization", bearer);
                    CustomerWishlistModel customerWishlistModel = new CustomerWishlistModel();
                    customerWishlistModel.ProductId = productId;
                    customerWishlistModel.CustomerId = customerId;

                    var response = httpClient.PostAsJsonAsync("api/ProceedToBuy/WishList", customerWishlistModel);

                    response.Wait();
                    var result = response.Result;

                    if (!result.IsSuccessStatusCode)
                    {
                        _log4net.Info("ProceedToBuy microservice failed");
                        return false;
                    }

                }
                catch (Exception)
                {
                    _log4net.Error("ProceedToBuy Microservice is Down!!");
                }
                return true;
            }
        }

        /*
         * Get All Vendor Who Are Having Stock For A Product
         * Input : JWT Token, Product Id
         * Output : VendorModel
         */
        public List<VendorModel> GetVendors(string token)
        {
            string bearer = String.Format("Bearer {0}", token);
            List<VendorModel> vendors = null;
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(vendorUri);
                try
                {
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Add("Authorization", bearer);

                    var response = httpClient.GetAsync("api/Vendor/GetVendor");

                    response.Wait();
                    var result = response.Result;

                    if (!result.IsSuccessStatusCode)
                    {
                        _log4net.Info("Vendor microservice failed");

                        return null;
                    }
                    var ApiResponse = result.Content.ReadAsAsync<List<VendorModel>>();
                    ApiResponse.Wait();
                    vendors = ApiResponse.Result;
                }
                catch (Exception)
                {
                    _log4net.Error("Vendor Microservice is Down!!");
                }
            }
            return vendors;
        }

        /*
         * Get All Vendor Who Are Having Stock For A Product
         * Input : JWT Token, Product Id
         * Output : VendorModel
         */
        public List<VendorModel> GetVendorsById(string token, int id)
        {
            string bearer = String.Format("Bearer {0}", token);
            List<VendorModel> vendors = null;
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(vendorUri);
                try
                {
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Add("Authorization", bearer);

                    var response = httpClient.GetAsync("api/Vendor/GetVendorByProductID/" + id);

                    response.Wait();
                    var result = response.Result;

                    if (!result.IsSuccessStatusCode)
                    {
                        _log4net.Info("Vendor microservice failed");

                        return null;
                    }
                    var ApiResponse = result.Content.ReadAsAsync<List<VendorModel>>();
                    ApiResponse.Wait();
                    vendors = ApiResponse.Result;
                }
                catch (Exception)
                {
                    _log4net.Error("Vendor Microservice is Down!!");
                }
            }
            return vendors;
        }

        /*
         * Update The Product Quantity When Product Is Added To Cart
         * Input : JWT Token, ProductId, VendorId
         * Output : True or False
         */
        public Boolean UpdateQuantity(string token, int productid, int vendorid)
        {
            _log4net.Info("Update Quantity");

            string bearer = String.Format("Bearer {0}", token);

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(vendorUri);
                try
                {
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Add("Authorization", bearer);

                    VendorProductModel vendorProductModel = new VendorProductModel();
                    vendorProductModel.ProductId = productid;
                    vendorProductModel.VendorId = vendorid;

                    var response = httpClient.PostAsJsonAsync("api/Vendor/UpdateVendorStockQuantity", vendorProductModel);

                    response.Wait();
                    var result = response.Result;

                    if (!result.IsSuccessStatusCode)
                    {
                        _log4net.Info("Vendor microservice failed");
                        return false;
                    }

                }
                catch (Exception)
                {
                    _log4net.Error("Vendor Microservice is Down!!");
                }
                return true;
            }

        }

        /*
         * Get Detail Of Cart
         * Input : JWT Token, CustomerId
         * Output : CartModel
         */
        public List<CartModel> GetCartDetail(string token,int id)
        {
            string bearer = String.Format("Bearer {0}", token);
            List<CartModel> carts = null;
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(proceedToBuyUri);
                try
                {
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Add("Authorization", bearer);

                    var response = httpClient.GetAsync("api/ProceedToBuy/GetCartDetails/" + id);

                    response.Wait();
                    var result = response.Result;
                    _log4net.Info(result);
                    if (!result.IsSuccessStatusCode)
                    {
                        _log4net.Info("ProceedToBuy microservice failed");

                        return null;
                    }
                    var ApiResponse = result.Content.ReadAsAsync<List<CartModel>>();
                    ApiResponse.Wait();

                    carts = ApiResponse.Result;
                }
                catch (Exception)
                {
                    _log4net.Error("ProceedToBuy Microservice is Down!!");
                }
            }
            return carts;
        }

        
        /*
         * Add All the product To Cart
         * Input : JWT Token, CartModel
         * Output : Ture or False
         */
        [HttpPost]
        public Boolean AddCart(string token,CartModel cart)
        {
            string bearer = String.Format("Bearer {0}", token);
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(proceedToBuyUri);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Add("Authorization", bearer);

                    var responseTask = client.PostAsJsonAsync<CartModel>("api/ProceedToBuy/AddtoCart", cart);

                    responseTask.Wait();

                    var result = responseTask.Result;

                    var content = result.Content.ReadAsStringAsync();

                    content.Wait();

                    return true;
                       
                }
            }
            catch(Exception)
            {
                _log4net.Error("Proceed To Buy cant post!!");
            }
            return false;
        }

        /*
         * Delete a Product From Cart
         * Input : JWT Token, CartId
         */
        public void DeleteCartProduct(string token, int CartId)
        {
            string bearer = String.Format("Bearer {0}", token);
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(proceedToBuyUri);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Add("Authorization", bearer);
                    var responseTask = client.DeleteAsync("api/ProceedToBuy/DeleteCartProduct/" + CartId);
                    responseTask.Wait();
                    var result = responseTask.Result;
                }
            }
            catch (Exception)
            {
                _log4net.Error("Delete Cart !!");
            }
        }

        /*
         * Delete all Product From Cart
         * Input : JWT Token, CustomerId
         */
        public void Checkout(string token, int CustomerId)
        {
            string bearer = String.Format("Bearer {0}", token);
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(proceedToBuyUri);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Add("Authorization", bearer);
                    var responseTask = client.DeleteAsync("api/ProceedToBuy/DeleteCartById/" + CustomerId);
                    responseTask.Wait();
                    var result = responseTask.Result;
                }
            }
            catch (Exception)
            {
                _log4net.Error("Clear all cart and wishlist item !!");
            }
        }

    }
}
