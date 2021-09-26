using NUnit.Framework;
using Moq;
using System.Collections.Generic;
using Product_Service.Model;
using Product_Service.Controllers;
using Product_Service.Repository;
using System.Linq;
using System;
using Microsoft.AspNetCore.Mvc;

namespace ProductServiceTest
{
    public class Tests
    {
        List<ProductModel> prod; 
        ProductRatingModel prodModel;
        int success = 1;
        int failure = 0;

        //Setting Up the Database for the testing
        [SetUp]
        public void Setup()
        {
             prod = new List<ProductModel>()
            {
                new ProductModel() { Id = 1, Price = 2000, Name = "Shoes", Description = "Some example text.", Image_Name = "1.jfif", Rating = 2 },
                new ProductModel() { Id = 2, Price = 900, Name = "Watch", Description = "Some example text.", Image_Name = "1.jfif", Rating = 3 }

            };
            prodModel = new ProductRatingModel() { Id = 1, Rating = 2 };


        }

        //Test 1. For Positive GetAllProducts
        [Test]
        public void GetAllProducts_ReturnsOkRequest()
        {
            //Create the object
            var mock = new Mock<IProductRepository>();
            //Specify and setup the function you want to test
            mock.Setup(m => m.GetAllProduct()).ReturnsAsync(prod);
            ProductController obj = new ProductController(mock.Object);
            var data = obj.GetAllProducts().Result;
            var res = data as ObjectResult;
            Assert.AreEqual(200, res.StatusCode);
        }

        //Test 2. For ReturnNotNull GetAllProducts
        [Test]
        public void GetAllProducts_ReturnsNotNullList()
        {
            //Create the object
            var mock = new Mock<IProductRepository>();
            ProductController obj = new ProductController(mock.Object);
            var data = obj.GetAllProducts();
            Assert.IsNotNull(data);
        }

        //Test 3. For Positive SearcProducthById
        [Test]
        public void SearchProductById_ReturnOkRequest()
        {
            int id = 1;
            //Create the object
            var mock = new Mock<IProductRepository>();
            //Specify and setup the function you want to test
            mock.Setup(x => x.SearchProductByID(id)).ReturnsAsync((prod.Where(x => x.Id == id)).FirstOrDefault());
            ProductController obj = new ProductController(mock.Object);
            var data = obj.SearchProductById(id).Result;
            var res = data as ObjectResult;
            Assert.AreEqual(202, res.StatusCode);
        }

        //Test 4. For Negative SearchProductById

        [Test]
        public void SearchProductById_ReturnNotFoundResult()
        {
            int id = 5;
            //Create the object
            var mock = new Mock<IProductRepository>();
            //Specify and setup the function you want to test
            mock.Setup(x=>x.SearchProductByID(id)).ReturnsAsync((prod.Where(x=>x.Id==id)).FirstOrDefault());
            try
            {
                ProductController obj = new ProductController(mock.Object);
                var data = obj.SearchProductById(id).Result;
            }
            catch(Exception e)
            {
                Assert.AreEqual("Product not found", e.Message);
            }

        }

        //Test 5. For Positive SearchProductByName
        [Test]
        public void SearchProductByName_ValidInput_ReturnsOkRequest()
        {
            string name = "Shoes";
            //Create the object
            var mock = new Mock<IProductRepository>();
            //Specify and setup the function you want to test
            mock.Setup(x => x.SearchProductByName(name)).ReturnsAsync((prod.Where(x => x.Name == name)).FirstOrDefault());
            ProductController obj = new ProductController(mock.Object);
            var data = obj.searchProductByName(name).Result;
            var res = data as ObjectResult;
            Assert.AreEqual(202, res.StatusCode);
        }

        //Test 6. For Negative SearchProductByName
        [Test]
        public void SearchProductByName_InvalidInput_ReturnsNotFoundResult()
        {
            string name = "Wallet";
            //Create the object
            var mock = new Mock<IProductRepository>();
            //Specify and setup the function you want to test
            mock.Setup(x => x.SearchProductByName(name)).ReturnsAsync((prod.Where(x => x.Name == name)).FirstOrDefault());

            try
            {
                ProductController obj = new ProductController(mock.Object);
                var data = obj.searchProductByName(name).Result;

            }
            catch(Exception e)
            {
                Assert.AreEqual("Product Not Found", e.Message);
            }
        }

        //Test 7. For Positive AddProductRating
        [Test]
        public void AddProductRating_ValidInput()
        {
            int id = 1;
            int rating = 4;
            prodModel.Rating = rating;
            //Create the object
            var mock = new Mock<IProductRepository>();
            //Specify and setup the function you want to test
            mock.Setup(x => x.AddProductRating(id, rating)).ReturnsAsync(success);
            ProductController obj = new ProductController(mock.Object);
            var data = obj.AddProductRating(prodModel).Result;
            var res = data as ObjectResult;



            Assert.AreEqual(200, res.StatusCode);
        }

        //Test 8. ForNegative AddProductRating
        [Test]
        public void AddProductRating_InvalidInput()
        {
            int id = 9;
            int rating = 4;
            prodModel.Rating = rating;
            //Create the object
            var mock = new Mock<IProductRepository>();
            //Specify and setup the function you want to test
            mock.Setup(x => x.AddProductRating(id, prodModel.Rating)).ReturnsAsync(failure);
            try
            {
                ProductController obj = new ProductController(mock.Object);

                var data = obj.AddProductRating(prodModel).Result;
            }
            catch(Exception e)
            {
                Assert.AreEqual("Product not found for the given productID...no rating added", e.Message);
            }
        }
    }
}