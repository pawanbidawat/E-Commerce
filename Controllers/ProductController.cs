using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TemporalBoxApi.Interfaces;
using TemporalBoxApi.Models;

namespace TemporalBoxApi.Controllers
{
    [EnableCors("AllowSpecificOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProduct _product;
        public ProductController(IProduct product)
        {
            _product = product;
        }

        [HttpGet("GetAllProduct")]
        public PagedProductData GetAllProducts([FromQuery]Paging paging ) {
            if (!string.IsNullOrEmpty(paging.ColumnName))
            {
              
                var data = _product.GetFilterProduct(paging);
                return data;
            }
            else
            {
                var allData = _product.GetAllProducts(paging);
                return allData;
            }
        }

        [HttpPost("AddProduct")]
        public void AddProduct(Product product) { 
        _product.AddProduct(product);
        }

        [HttpGet("GetProductById")]
        public Product GetProductById(int id)
        {
            var data = _product.GetProduct(id);
            return data;
        }

        [HttpGet("GetProductBySubCategoryId")]
        public List<Product> GetProductBySubCategoryId(int id)
        {
            var data = _product.GetProductBySubCategoryId(id).ToList();
            return data;
        }

        [HttpGet("GetFilterProduct")]
        public PagedProductData GetFilterProduct([FromQuery]Paging paging) {
            var data = _product.GetFilterProduct(paging);
            return data;
        }


        [HttpPut("UpdateProduct")]
        public void UpdateProduct(int id ,Product product)
        {
            _product.UpdateProduct(id, product);
        }

        [HttpDelete("DeleteProduct")]
        public bool DeleteProduct(int id)
        {
            var data = _product.DeleteProduct(id);
            return data;
        }

    }
}
