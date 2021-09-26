using Retail_Product_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Retail_Product_Management_System.Services
{
    public interface IRepository
    {
        public List<ProductModel> GetProducts(string token);

        public ProductModel GetProductsByName(string token, string name);

        public ProductModel GetProductsById(string token, int id);

        public Boolean AddRating(string token,int Id, int rating);

        public List<CustomerWishlistModel> GetWishlists(string token, int id);

        public Boolean PostWishlists(string token, int productId, int customerId);

        public List<VendorModel> GetVendors(string token);

        public List<VendorModel> GetVendorsById(string token, int id);

        public Boolean UpdateQuantity(string token, int productid, int vendorid);

        public List<CartModel> GetCartDetail(string token, int id);

        public Boolean AddCart(string token, CartModel cart);

        public void DeleteCartProduct(string token, int CartId);

        public void Checkout(string token, int CustomerId);
    }
}
