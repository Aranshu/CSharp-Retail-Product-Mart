using Microsoft.EntityFrameworkCore;
using ProceedToBuy.API.Data;
using ProceedToBuy.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProceedToBuy.API.Repository
{
    public class ProceedToBuyRepository : IProceedToBuyRepository
    {
        /*
         * ProceedtoBuyContext Onject
         */
        ProceedToBuyContext _context;

        /*
         * Dependency Injection
         */
        public ProceedToBuyRepository(ProceedToBuyContext context)
        {
            _context = context;
        }

        /*
         * Fuction To get WishList
         * Input : CustumerId
         * Output CustomerWishList
         */
        public List<CustomerWishlistModel> GetWishlist(int CustomerId)
        {
            return _context.CustomerWishlists.Where(v => v.CustomerId == CustomerId).ToList();
        }

        /*
         * Enter Product Into WishList
         * Input : WishList Model
         * Output : Status
         */
        public bool AddToWishList(CustomerWishlistModel CustomerWishlistModel)
        {
            CustomerWishlistModel vendorWishlist = new CustomerWishlistModel();
            vendorWishlist.CustomerId = CustomerWishlistModel.CustomerId;
            vendorWishlist.ProductId = CustomerWishlistModel.ProductId;
            vendorWishlist.DateAddedToWishlist = DateTime.Now;
            _context.CustomerWishlists.Add(vendorWishlist);
            _context.SaveChanges();
            return true;
        }

        /*
         * Get All Cart Value Just For testing
         */
        public List<CartModel> GetCart()
        {
            return _context.Carts.ToList();
        }

        /*
         * Get Card Detail 
         * Input : Customer Id
         * Output : CartModel
         */
        public List<CartModel> getCartDetails(int CustomerId)
        {
            return _context.Carts.Where(v => v.CustomerId == CustomerId).ToList();
        }

        /*
         * Add Product To Cart
         * Input : CartModel
         * Output : Status
         */
        public bool AddProductToCart(CartModel cartModel)
        {
            CartModel cart = new CartModel();
            cart.CartId = cartModel.CartId;
            cart.CustomerId = cartModel.CustomerId;
            cart.VendorId = cartModel.VendorId;
            cart.ProductId = cartModel.ProductId;
            cart.DeliveryDate = cartModel.DeliveryDate;
            cart.Zipcode = cartModel.Zipcode;
            _context.Carts.Add(cart);
            _context.SaveChanges();
            return true;
        }

        /*
         * Delete a Product From Cart
         * Input : CustomerId, ProductId
         * Output : Status
         */
        public bool DeleteCart(int cartid)
        {
            CartModel cart = _context.Carts.Where(x => x.CartId == cartid).SingleOrDefault();
            _context.Carts.Remove(cart);
            _context.SaveChanges();
            return true;
        }

        /*
         * Delete All Product In A  Cart
         * Input : CustomerId
         * Output : Status
         */
        public bool DeleteCartByIdOnly(int CustomerId)
        {
            List<CartModel> cart = _context.Carts.Where(x => x.CustomerId == CustomerId).ToList();
            foreach(CartModel cartModel in cart)
            {
                _context.Carts.Remove(cartModel);
                _context.SaveChanges();
            }
            return true;
        }


    }
}

