using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ProceedToBuy.API.Controllers;
using ProceedToBuy.API.Models;
using ProceedToBuy.API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProceedToBuy_Test
{
    public class Tests
    {
        List<CartModel> carts;
        List<CustomerWishlistModel> wishList;

        //Setting Up the Database for the testing
        [SetUp]
        public void Setup()
        {
            carts = new List<CartModel>
            {
                new CartModel{CartId=1,ProductId=101,CustomerId=1,DeliveryDate=Convert.ToDateTime("2021/06/21"),VendorId=201,Zipcode=641008 },
                new CartModel{CartId=2,ProductId=102,CustomerId=1,DeliveryDate=Convert.ToDateTime("2021/06/21"),VendorId=202,Zipcode=641008 }
            };
            wishList = new List<CustomerWishlistModel>
            {
                new CustomerWishlistModel{Id=1,CustomerId=1,ProductId=101,DateAddedToWishlist=DateTime.Now}
            };
        }

        //Test 1. For GetWhishList
        [Test]
        public void GetWishList()
        {
            int customerId = 1;
            //Create the object
            Mock<IProceedToBuyRepository> moqRepo = new Mock<IProceedToBuyRepository>();
            //Specify and setup the function you want to test
            moqRepo.Setup(c => c.GetCart()).Returns(carts);
            List<CustomerWishlistModel> expected = (wishList.Where(w => w.CustomerId == customerId)).ToList();
            moqRepo.Setup(c => c.GetWishlist(customerId)).Returns(expected);
            ProceedToBuyController pbc = new ProceedToBuyController(moqRepo.Object);
            Assert.AreEqual(expected, pbc.GetWishList(customerId));
        }

        //Test 2. For AddToWhishList
        [Test]
        public void AddToWishList()
        {
            CustomerWishlistModel cm = new CustomerWishlistModel {CustomerId=1,ProductId=101 };
            //Create the object
            Mock<IProceedToBuyRepository> moqRepo = new Mock<IProceedToBuyRepository>();
            //Specify and setup the function you want to test
            moqRepo.Setup(c => c.AddToWishList(cm)).Returns(true);
            
            ProceedToBuyController _proceedToBuyCon = new ProceedToBuyController(moqRepo.Object);
            Assert.AreEqual(200, ((ObjectResult)_proceedToBuyCon.WishList(cm)).StatusCode);
        }

        //Test 3. For GetAllCarts
        [Test]
        public void GetAllCarts()
        {
            //Create the object
            Mock<IProceedToBuyRepository> moqRepo = new Mock<IProceedToBuyRepository>();
            //Specify and setup the function you want to test
            moqRepo.Setup(c => c.GetCart()).Returns(carts);
            ProceedToBuyController obj = new ProceedToBuyController(moqRepo.Object);
            var actual = obj.getCart();
            CollectionAssert.AreEqual(carts, actual);
        }

        //Test 4. For GetCartsDetailsByCustomerID
        [Test]
        public void GetCartsDetailsByCustomerID()
        {
            int customerId = 1;
            //Create the object
            Mock<IProceedToBuyRepository> moqRepo = new Mock<IProceedToBuyRepository>();
            //Specify and setup the function you want to test
            moqRepo.Setup(c => c.GetCart()).Returns(carts);
            List<CartModel> expected = (carts.Where(w => w.CustomerId == customerId)).ToList();
            moqRepo.Setup(c => c.getCartDetails(customerId)).Returns(expected);
            ProceedToBuyController obj = new ProceedToBuyController(moqRepo.Object);
            var actual = obj.getCart();
            CollectionAssert.AreEqual(carts, actual);
        }

        //Test 5. For AddToCart
        [TestCase]
        public void AddToCart()
        {
            CartModel input = new CartModel { CartId = 1, ProductId = 101, CustomerId = 1, DeliveryDate = Convert.ToDateTime("2021/06/21"), VendorId = 201, Zipcode = 641008 };
            //Create the object
            Mock<IProceedToBuyRepository> moqRepo = new Mock<IProceedToBuyRepository>();
            //Specify and setup the function you want to test
            moqRepo.Setup(c => c.AddProductToCart(input)).Returns(true);
           
            ProceedToBuyController _proceedToBuyCon = new ProceedToBuyController(moqRepo.Object);
           
            Assert.AreEqual(200, ((ObjectResult)_proceedToBuyCon.AddToCart(input)).StatusCode);
        }

        //Test 6. For Positive Case For DeleteAllByCustomerID
        [Test]
        public void DeleteAllByCustomerID_Positive()
        {
            int customerId = 1;
            //Create the object
            Mock<IProceedToBuyRepository> moqRepo = new Mock<IProceedToBuyRepository>();
            //Specify and setup the function you want to test
            moqRepo.Setup(c => c.DeleteCartByIdOnly(customerId)).Returns(true);
            
            ProceedToBuyController _proceedToBuyCon = new ProceedToBuyController(moqRepo.Object);
            Assert.AreEqual("Success", ((ObjectResult)_proceedToBuyCon.DeleteCartById(customerId)).Value);
        }

        //Test 7. For Negative Case For DeleteAllByCustomerID
        [Test]
        public void DeleteAll_Negative()
        {
            int customerId = -14;
            //Create the object
            Mock<IProceedToBuyRepository> moqRepo = new Mock<IProceedToBuyRepository>();
            //Specify and setup the function you want to test
            moqRepo.Setup(c => c.DeleteCartByIdOnly(customerId)).Returns(false);
           
            ProceedToBuyController _proceedToBuyCon = new ProceedToBuyController(moqRepo.Object);
            Assert.AreEqual("Request Failed", ((ObjectResult)_proceedToBuyCon.DeleteCartById(customerId)).Value);
        }

        //Test 8. For Positive Case For DeleteCartByCartId
        [Test]
        public void DeleteCartByCartId_Positive()
        {
            int cartId = 1;
            //Create the object
            Mock<IProceedToBuyRepository> moqRepo = new Mock<IProceedToBuyRepository>();
            //Specify and setup the function you want to test
            moqRepo.Setup(c => c.DeleteCart(cartId)).Returns(true);
            
            ProceedToBuyController _proceedToBuyCon = new ProceedToBuyController(moqRepo.Object);
            Assert.AreEqual("Success", ((ObjectResult)_proceedToBuyCon.DeleteCartProduct(cartId)).Value);
        }

        //Test 9. For Negative Case For DeleteCartByCartId
        [Test]
        public void DeleteCartByCartId_Negative()
        {
            int cartId = -14;
            //Create the object
            Mock<IProceedToBuyRepository> moqRepo = new Mock<IProceedToBuyRepository>();
            //Specify and setup the function you want to test
            moqRepo.Setup(c => c.DeleteCart(cartId)).Returns(false);
            
            ProceedToBuyController _proceedToBuyCon = new ProceedToBuyController(moqRepo.Object);
            Assert.AreEqual("Request Failed", ((ObjectResult)_proceedToBuyCon.DeleteCartProduct(cartId)).Value);
        }
    }
}