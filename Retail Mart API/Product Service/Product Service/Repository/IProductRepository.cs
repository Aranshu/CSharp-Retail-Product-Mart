using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Product_Service.Model;

namespace Product_Service.Repository
{
    public interface IProductRepository
    {
        Task<List<ProductModel>> GetAllProduct();
        Task<ProductModel> SearchProductByID(int id);
        Task<ProductModel> SearchProductByName(string name);
        Task<int> AddProductRating(int id, int rating);
    }
}
