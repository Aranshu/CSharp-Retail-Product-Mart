using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;
using Vendors.API.Data;
using Vendors.API.Models;

namespace Vendors.API.Repository
{
    public class VendorRepository :IVendorRespository
    {
        /*
         * Vendor Object
         */
        VendorContext _context;

        /*
         * Dependency Injection
         */
        public VendorRepository(VendorContext context)
        {
            _context = context;
        }

        /*
         * Vendor By Vendor Id
         * Output : Vendor Model List
         */
        public List<Vendor> GetVendor()
        {
            return _context.Vendor.ToList();

        }

        /*
         * Vendor By Vendor Id
         * Input : VendorId
         * Output : Vendor Model
         */
        public Vendor GetVendorByVendorID(int vendorId)
        {
            return  _context.Vendor.Where(v => v.Id == vendorId).FirstOrDefault();

        }

        /*
         * Vendor Detail By Product Id
         * Input : ProductId
         * Output : Vendor Model
         */
        public IEnumerable<Vendor> GetVendorByProductID(int productid)
        {
            try
            {
                IEnumerable<VendorStock> vendorStockObject = _context.VendorStock.Include(v => v.Vendor).Where(v => v.ProductId == productid && v.HandInStocks > 0);

                if (!vendorStockObject.Any())
                {
                    return null;
                }
                else
                {
                    return vendorStockObject.Select(v => v.Vendor);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /*
         * Update Vendor Stock Quantity
         * Input : ProductId, VendorId
         */
        public void UpdateVendorStockQuantity(int productid, int vendorid)
        {
            VendorStock vendorStockObject = _context.VendorStock.Where(v => v.VendorId == vendorid && v.ProductId == productid).FirstOrDefault();
            vendorStockObject.HandInStocks--;
            _context.SaveChanges();
        }


    }
}
