using Microsoft.AspNetCore.Mvc;
using ProceedToBuy.API.Models;
using ProceedToBuy.API.Repository;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace ProceedToBuy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProceedToBuyController : ControllerBase
    {
        /*
         * IProceedToBuy Object
         */
        IProceedToBuyRepository _repository;

        /*
        * logging Object
        */
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(ProceedToBuyController));

        /*
         * Dependency Injection
         */
        public ProceedToBuyController(IProceedToBuyRepository repository)
        {
            _repository = repository;
            _log4net.Info("Logger initiated");
        }

        /*
         * Fuction To get WishList
         * api/ProceedToBuy/GetWishList/
         * Input : CustumerId
         * Output CustomerWishList
         */
        [HttpGet("GetWishList/{Customerid:int}")]
        public IEnumerable<CustomerWishlistModel> GetWishList([FromRoute] int Customerid)
        {
            _log4net.Info("Get Customer WishList");
            return _repository.GetWishlist(Customerid);
        }

        /*
         * Enter Product Into WishList
         * api/ProceedToBuy/WishList
         * Input : WishList Model
         * Output : Status
         */
        [Route("WishList")]
        [HttpPost]
        public IActionResult WishList([FromBody] CustomerWishlistModel CustomerWishlistModel)
        {
            _log4net.Info("Save Customer WishList");
            _repository.AddToWishList(CustomerWishlistModel);
            return Ok("Success");
        }

        /*
         * Get All Cart Value Just For testing
         */
        [HttpGet]
        public IEnumerable<CartModel> getCart()
        {
            return _repository.GetCart();
        }

        /*
         * Get Card Detail 
         * api/ProceedToBuy/GetCartDetails/
         * Input : Customer Id
         * Output : CartModel
         */
        [HttpGet("GetCartDetails/{customerid:int}")]
        public IEnumerable<CartModel> GetCartDetails([FromRoute] int customerid)
        {
            _log4net.Info("Get Cart Detail With Help of CustomerId");
            return _repository.getCartDetails(customerid);
        }


        /*
         * Add Product To Cart
         * api/ProceedToBuy/AddtoCart
         * Input : CartModel
         * Output : Status
         */
        [HttpPost("AddtoCart")]
        public IActionResult AddToCart([FromBody] CartModel cartModel)
        {
           var res =  _repository.AddProductToCart(cartModel);
            if (res)
            {
                _log4net.Info("Add To Cart Success");
                return Ok("Success");
            }
            _log4net.Info("Add To Cart Success");
            return BadRequest("Request Failed");
        }

        /*
         * Delete a Product From Cart
         * api/ProceedToBuy/DeleteCartProduct/
         * Input : CustomerId, ProductId
         * Output : Status
         */
        [HttpDelete("DeleteCartProduct/{cartid:int}")]
        public IActionResult DeleteCartProduct([FromRoute] int cartid)
        {
            var res = _repository.DeleteCart(cartid);
            if (res)
            {
                _log4net.Info("Deleting Cart Product Success");
                return Ok("Success");
            }
            _log4net.Info("Deleting Cart Product Failed");
            return BadRequest("Request Failed");
        }

        /*
         * Delete All Product In A  Cart
         * api/ProceedToBuy/DeleteCartById/
         * Input : CustomerId
         * Output : Status
         */
        [HttpDelete("DeleteCartById/{customerid:int}")]
        public IActionResult DeleteCartById([FromRoute] int customerid)
        {
            var res = _repository.DeleteCartByIdOnly(customerid);
            if (res)
            {
                _log4net.Info("Deleting Cart Success");
                return Ok("Success");
            }
            _log4net.Info("Deleting Cart Failed");
            return BadRequest("Request Failed");
        }
    }
}
