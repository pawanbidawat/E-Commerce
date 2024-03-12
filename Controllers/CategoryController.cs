using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TemporalBoxApi.Interfaces;
using TemporalBoxApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TemporalBoxApi.Controllers
{
   
    [EnableCors("AllowSpecificOrigin")]  
    [Route("api/[controller]")]
    [ApiController]
   
    public class CategoryController : ControllerBase
    {
        private readonly ICategory _category;
        public CategoryController(ICategory category)
        {
             _category = category;
        }
        // [Authorize(Policy = "Role")]
       // [Authorize(Roles = "admin")]
        [Authorize]
        [HttpGet("GetAllCategory")]
        public List<Categories> GetCat()
        {
            var userRole = HttpContext.Request.Headers["X-UserRole"].ToString();
            var data = _category.GetCategorieDetails();
            return data;
        }
        [HttpGet("GetSubCategory")]
        public List<SubCategory> GetSubCategory(int id)
        {
            var data = _category.GetSubCategory(id);
            return data;
        }

        [HttpGet("GetSubCategoryWithId")]
        public List<SubCategory> GetSubCategoryWithId(int id)
        {
            var data = _category.GetSubCategoryWithId(id);
            return data;
        }

        [EnableCors("AllowSpecificOrigin")]
        [HttpPost("AddCategory")]
        public Categories AddCategory(string category)
        {
            var data = _category.AddCategory(category);
            return data;
        }




        // GET category with id
        [HttpGet("GetCategory")]
        public Categories GetCategory(int id)
        {
           var data = _category.GetCategory(id);
            return data;
        }

      

        // PUT api/<Cat_SubController>/5
        [HttpPut("UpdateMethod")]
        public Categories update(int id, [FromBody] Categories value)
        {
            var data = _category.UpdatCategorye(id,value);
            return data;
         
        }

        // DELETE api
        [HttpDelete("DeleteCategory")]
        public bool DeleteCategory(int id)
        {
            var data = _category.DeleteCategory(id);
            return data;
        }

        //deleting subCategory
        [HttpDelete("DeleteSubCategory")]
        public bool DeleteSubCategory(int id)
        {
            var data = _category.DeleteSubCategory(id);
            return data;
        }

        [HttpPost("AddSubcategory")]
        public void AddSubcategory(int categoryId, string subCategoryName)
        {
            _category.AddSubcategory(categoryId, subCategoryName);
        }

        //action method for updating the categoryName
        [HttpPut("UpdateCategory")]
        public void UpdateCategory(int Id, string CategoryName)
        {
            _category.UpdateCategory(Id, CategoryName);
        }

        [HttpPut("UpdateSubCategory")]
        public void UpdateSubCategory(int Id, string SubCategoryName)
        {
            _category.UpdateSubCategory(Id, SubCategoryName);
        }
    }
}
