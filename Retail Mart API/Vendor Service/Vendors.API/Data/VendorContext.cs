using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vendors.API.Models;

namespace Vendors.API.Data
{
    public class VendorContext :DbContext
    {
        public VendorContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<Vendor> Vendor { get; set; }
        public DbSet<VendorStock> VendorStock { get; set; }
    }
}
