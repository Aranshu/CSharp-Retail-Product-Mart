using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Vendors.API.Controllers;
using Vendors.API.Models;
using Vendors.API.Repository;

namespace VendorsAPI_Test
{
    public class Tests
    {
        List<Vendor> vendors;
        List<VendorStock> vendorStocks;
        //Mock<VendorRepository> moqRepo;
        [SetUp]
        public void Setup()
        {
            vendors = new List<Vendor>()
            {
                new Vendor() { Id = 201, Name="DelhiMotoShop", DeliveryCharge = 45, Rating=  5 },
                new Vendor() { Id=202, Name="HydMotoShop", DeliveryCharge= 50, Rating= 4 }
            };
            vendorStocks = new List<VendorStock>()
            {
                 new VendorStock() { Id = 1, ProductId = 101, VendorId = 201, HandInStocks= 24, ReplinshmentDate= Convert.ToDateTime(" 2021 - 02 - 02"),Vendor=new Vendor() { Id = 201, Name="DelhiMotoShop", DeliveryCharge = 45, Rating=  5 }},
                 new VendorStock() { Id = 2, ProductId = 1, VendorId = 202, HandInStocks = 24, ReplinshmentDate = Convert.ToDateTime(" 2021 - 02 - 02"),Vendor=new Vendor() { Id = 201, Name="DelhiMotoShop", DeliveryCharge = 45, Rating=  5 } }
            };
        }

        [Test]
        public void GetVendor_ReturnAllVendor()
        {
            var mock = new Mock<IVendorRespository>();
            mock.Setup(v => v.GetVendor()).Returns(vendors);
            VendorController vc = new VendorController(mock.Object);
            var result = vc.GetVendor();
            CollectionAssert.AreEqual(vendors, result);
        }

        [Test]
        public void TestControllerGetByProductId_ReturnPositive()
        {
            int id = 11;
            var mock = new Mock<IVendorRespository>();
            mock.Setup(v => v.GetVendorByProductID(id)).Returns(vendorStocks.Where(v => v.ProductId == id && v.HandInStocks > 0).Select(ve => ve.Vendor));
            //    (vendorStocks.Where(v => v.ProductId == id)).FirstOrDefault();
            //
            // mock.Setup(v => v.GetVendorByVendorID(id)).Returns(VendorStock.Include(v => v.Vendor).Where(v => v.ProductId == id && v.HandInStocks > 0));
            VendorController vc = new VendorController(mock.Object);
            var actual = vc.GetVendorByProductID(id);
            ObjectResult okObjectResult = (ObjectResult)actual;
            Assert.AreEqual(202, okObjectResult.StatusCode);
        }
        [Test]
        public void TestControllerUpdateVendorStockQuantity_ReturnOk()
        {
            int productid = 1;
            int vendorid = 202;

            VendorProductModel vpm = new VendorProductModel() { ProductId = 1, VendorId = 202 };
            var mock = new Mock<IVendorRespository>();
            mock.Setup(v => v.UpdateVendorStockQuantity(productid, vendorid));

            VendorController vc = new VendorController(mock.Object);
            var actual = vc.UpdateVendorStockQuantity(vpm);
            ObjectResult okObjectResult = (ObjectResult)actual;
            Assert.AreEqual(200, okObjectResult.StatusCode);
        }
    }
}