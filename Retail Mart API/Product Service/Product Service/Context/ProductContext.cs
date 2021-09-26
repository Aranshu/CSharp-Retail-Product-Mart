using Microsoft.EntityFrameworkCore;
using Product_Service.Model;

namespace Product_Service.Context
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> option)
            : base(option)
        {
        }
        public DbSet<ProductModel> ProductModels { get; set; }
    }
}
