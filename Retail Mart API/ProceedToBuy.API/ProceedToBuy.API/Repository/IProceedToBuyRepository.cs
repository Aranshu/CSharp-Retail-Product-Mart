using ProceedToBuy.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProceedToBuy.API.Repository
{
    public interface IProceedToBuyRepository
    {
        public List<CustomerWishlistModel> GetWishlist(int id);

        bool AddToWishList(CustomerWishlistModel vendorWishlistModel);

        public List<CartModel> GetCart();

        public List<CartModel> getCartDetails(int CustomerId);

        bool AddProductToCart(CartModel cartModel);

        bool DeleteCart(int cartid);

        bool DeleteCartByIdOnly(int CustomerId);
    }
}
