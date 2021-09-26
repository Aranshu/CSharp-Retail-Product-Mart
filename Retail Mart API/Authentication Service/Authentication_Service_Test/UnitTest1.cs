using Authentication_Service.Controllers;
using Authentication_Service.Model;
using Authentication_Service.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Authentication_Service_Test
{
    public class Tests
    {
        //Object Creation
        private RegisterModel users;
        private LoginModel login;
        private DetailModel usersDetail;

        //Setting Up Database
        [SetUp]
        public void Setup()
        {
            login = new LoginModel { Email = "aranshu552@gmail.com", Password = "aranshu" };
            usersDetail = new DetailModel { CustomerId = 1 , FirstName = "admin", Address = "Testing Address", Token = "1234" };
            users = new RegisterModel { FirstName = "admin", LastName = "one", Email = "abc@abc.com", Password = "admin", Address = "Testing Address" };
        }

        [TearDown]
        public void TearDown()
        {
            login = null;
            usersDetail = null;
            users = null;
        }
        

        //Positive Test Case Login
        [Test]
        public void LoginTest_Positive()
        {
            
            Mock<IAccountRepository> mockObject = new Mock<IAccountRepository>();

            mockObject.Setup(c => c.Login(login)).Returns(usersDetail);

            AccountController account = new AccountController(mockObject.Object);
            var data = account.Login(login);
            var result = data as ObjectResult;

            Assert.AreEqual(200, result.StatusCode);
        }

        //Negative Test Case Login
        [Test]
        public void LoginTest_Negative()
        {
            Mock<IAccountRepository> mockObject = new Mock<IAccountRepository>();
            
            usersDetail = null;
            mockObject.Setup(c => c.Login(login)).Returns(usersDetail);

            AccountController account = new AccountController(mockObject.Object);
            var data = account.Login(login);
            var result = data as ObjectResult;

            Assert.AreEqual(401, result.StatusCode);
        }

        //Registeration Test
        [Test]
        public void RegisterUser()
        {   
            Mock<IAccountRepository> mock = new Mock<IAccountRepository>();
            
            mock.Setup(x => x.Register(users)).Returns(users.Id);

            AccountController obj = new AccountController(mock.Object);
            var data = obj.Register(users);
            var result = data as ObjectResult;
            
            Assert.AreEqual(200, result.StatusCode);
        }
    }
}