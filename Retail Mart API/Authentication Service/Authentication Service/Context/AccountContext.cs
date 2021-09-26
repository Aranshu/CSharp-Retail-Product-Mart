using Authentication_Service.Model;
using Microsoft.EntityFrameworkCore;

namespace Product_Service.Context
{
    public class AccountContext : DbContext
    {
        public AccountContext(DbContextOptions<AccountContext> option)
            : base(option)
        {
        }
        public DbSet<RegisterModel> RegisterModels { get; set; }
    }
}
