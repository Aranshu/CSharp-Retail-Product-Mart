using Microsoft.EntityFrameworkCore;
using ProceedToBuy.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProceedToBuy.API.Data
{
    public class ProceedToBuyContext :DbContext
    {
        public ProceedToBuyContext(DbContextOptions<ProceedToBuyContext> options) : base(options)
        {

        }

        public DbSet<CartModel> Carts { get; set; }
        public DbSet<CustomerWishlistModel> CustomerWishlists { get; set; }
    }
}
