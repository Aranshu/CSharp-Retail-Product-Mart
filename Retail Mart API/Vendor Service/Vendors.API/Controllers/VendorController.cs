using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vendors.API.Models;
using Vendors.API.Repository;

namespace Vendors.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VendorController : ControllerBase
    {
        /*
         * IVendorRepository Object
         */
        private readonly IVendorRespository _context;

        /*
         * Logging Object
         */
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(VendorController));

        /*
         * Dependency Injection
         */
        public VendorController(IVendorRespository vendor)
        {
            _context = vendor;
            _log4net.Info("Logger initiated");
        }

        /*
         * Get All Vendor
         * api/Vendor/GetVendor
         * Output : Vendor Model
         */

        [HttpGet("GetVendor")]
        public IEnumerable<Vendor> GetVendor()
        {
            _log4net.Info("Vendor Detail");
            return _context.GetVendor();

        }

        /*
         * Search Vendor by vendor ID
         * api/Vendor/GetVendorByVendorID/
         * Input : VendorId
         * Output : Vendor Model
         */

        [HttpGet("GetVendorByVendorID/{Id:int}")]
        public Vendor GetVendorByVendorID([FromRoute] int Id)
        {
            _log4net.Info("Vendor Detail With Help of Vendor ID");
            return _context.GetVendorByVendorID(Id);
        }

        /*
         * Return Vendor With Product in Stock
         * api/Vendor/GetVendorByProductID/
         * Input : Id 
         * Output : Status
         */
        [HttpGet("GetVendorByProductID/{Id:int}")]
        public IActionResult GetVendorByProductID([FromRoute]int Id)
        {
            _log4net.Info("Get Vendor By ProductID");
            return Accepted(_context.GetVendorByProductID(Id));
        }

        /*
         * Update Stock In Hand
         * api/Vendor/UpdateVendorStockQuantity
         * Input : VendorProductModel
         * Output : Status
         */
        [HttpPost("UpdateVendorStockQuantity")]
        public IActionResult UpdateVendorStockQuantity([FromBody] VendorProductModel vendorProductModel )
        {
            _log4net.Info("Updated Quantity");
            _context.UpdateVendorStockQuantity(vendorProductModel.ProductId, vendorProductModel.VendorId);
            return Ok("Success");
        }
    }
}
