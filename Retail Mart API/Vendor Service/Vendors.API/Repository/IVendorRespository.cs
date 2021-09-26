using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vendors.API.Data;
using Vendors.API.Models;

namespace Vendors.API.Repository
{
   public interface IVendorRespository
    {
        public List<Vendor> GetVendor();

        public Vendor GetVendorByVendorID(int vendorId);

        public IEnumerable<Vendor> GetVendorByProductID(int productid);

        public void UpdateVendorStockQuantity(int productid, int vendorid);

    }
}
