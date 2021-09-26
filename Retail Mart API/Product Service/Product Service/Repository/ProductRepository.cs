using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Product_Service.Context;
using Product_Service.Model;

namespace Product_Service.Repository
{
    public class ProductRepository:IProductRepository
    {
        private readonly ProductContext _productContext;

        /*
         * Dependency Injection
         */
        public ProductRepository(ProductContext productContext)
        {
            _productContext = productContext;
        }

        /*
         * Show All Products
         */
        public async Task<List<ProductModel>> GetAllProduct()
        {
            return await _productContext.ProductModels.ToListAsync();
        }

        /*
         * Add Rating to Product
         */
        public async Task<int> AddProductRating(int id, int rating)
        {
            var data = _productContext.ProductModels.Where(x => x.Id == id).FirstOrDefault();

            if (data == null)
            {
                return 0;
            }

            data.Rating = rating;

            _productContext.ProductModels.Update(data);
            await _productContext.SaveChangesAsync();

            return data.Id;
        }

        /*
         * Seach Product For a Particular Id
         */
        public async Task<ProductModel> SearchProductByID(int id)
        {
            return await _productContext.ProductModels.Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        /*
         * Seach Product For a Particular Name
         */

        public async Task<ProductModel> SearchProductByName(string name)
        {
            return await _productContext.ProductModels.Where(p => p.Name == name).FirstOrDefaultAsync();
        }

    }
}
