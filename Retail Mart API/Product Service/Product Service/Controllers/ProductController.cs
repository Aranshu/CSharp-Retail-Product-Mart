using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Product_Service.Model;
using Product_Service.Repository;

namespace Product_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        /*
         * IProductRepository Object
         */
        private readonly IProductRepository _productRepository;

        /*
         * logging Object
         */
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(ProductController));

        /*
         * Dependency Injection
         */
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            _log4net.Info("Logger initiated");
        }

        /*
         * Get All Product
         * api/Product/GetAllProducts
         * Output: Product object
         */
        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                _log4net.Info("Loading all available product");
                var product = await _productRepository.GetAllProduct();
                if (product == null)
                {
                    _log4net.Error("There are no product in the stock");
                    return NotFound("No Product is Available");
                }
                return Ok(product);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /*
         * Search Product By Id
         * api/Product/SearchProductById/
         * Input:id
         * Output: Product object
         */
        [HttpGet("SearchProductById/{id:int}")]
        public async Task<IActionResult> SearchProductById([FromRoute] int id)
        {
            _log4net.Info("Searching product by productId");
            var product = await _productRepository.SearchProductByID(id);
            if (product == null)
            {
                _log4net.Error("Product not found for the given productID");
                return NotFound("No Product is Available With ID : " + id);
            }
            return Accepted(product);
        }

        /*
         * Search Product By Name
         * api/Product/GetProductByName/
         * Input:Name
         * Output:Product object
         */
        [HttpGet("GetProductByName/{name}")]
        public async Task<IActionResult> searchProductByName([FromRoute] string name)
        {
            var product = await _productRepository.SearchProductByName(name);
            if (product == null)
            {
                _log4net.Info("Searching product by productName");
                return NotFound($"No Product is Available With Name : {name}");
            }
            return Accepted(product);
        }

        /*
         * Add Rating to Product
         * api/Product/AddProductRating/
         * Input: Id, Rating
         * Output: Success
         */
        [HttpPost("AddProductRating")]
        public async Task<IActionResult> AddProductRating([FromBody] ProductRatingModel productRatingModel)
        {
            var product = _productRepository.SearchProductByID(productRatingModel.Id);
            if(product==null)
            {
                _log4net.Error("Product not found for the given productID...no rating added");
                return NotFound("Product not found for the given productID...no rating added");
            }
            else 
            {
                _log4net.Info("Added Rating to the Product");
                var id = await _productRepository.AddProductRating(productRatingModel.Id, productRatingModel.Rating);
                if (id != 0)
                {
                    return Ok("Success");
                }
                return NotFound("Service not available");
            }
        }
    }
}
