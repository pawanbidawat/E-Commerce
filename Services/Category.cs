using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TemporalBoxApi.DTO;
using TemporalBoxApi.Interfaces;
using TemporalBoxApi.JwtContext;
using TemporalBoxApi.Models;


namespace TemporalBoxApi.Services
{
    public class Category : ICategory
    {
        private readonly Context _context;
        private readonly ILogger<Category> _logger;
        public Category(Context context, ILogger<Category> logger)
        {
            _context = context;
            _logger = logger;
        }

        public Categories AddCategory(string category)
        {
            var temp = new Categories() { CategoryName = category };
            var data = _context.Categories.Add(temp);
            _context.SaveChanges();
            return data.Entity;
        }

    
        public bool DeleteCategory(int id)
        {
            try
            {
                var data = _context.Categories.FirstOrDefault(x => x.CategoryId == id);
               if(data==null)
                {
                    throw new Exception("data not found");
                }
                _context.Categories.Remove(data);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        //adding delete subcategory
       public bool DeleteSubCategory(int id)
        {
            try
            {
                var data = _context.SubCategories.FirstOrDefault(x => x.SubCategoryId == id);
                if (data == null)
                {
                    throw new Exception("data not found");
                }
                _context.SubCategories.Remove(data);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }



        public List<Categories> GetCategorieDetails()
        {

        
            var data = _context.Categories.Include(x => x.SubCategoryList).ToList();
            return data;

        }

        public List<SubCategory> GetSubCategory(int id)
        {
            if (id != 0)
            {
                var data = _context.SubCategories.Where(x=>x.CategoryId == id).ToList();
                return data;               
            }
            else
            {
                return new();
            }
        }

        public List<SubCategory> GetSubCategoryWithId(int id)
        {
            if (id != 0)
            {
                var data = _context.SubCategories.Where(x => x.SubCategoryId == id).ToList();
                return data;
            }
            else
            {
                return new();
            }
        }
        public Categories GetCategory(int id)
        {
            if (id != 0 && id != null)
            {
                var data = _context.Categories.Find(id);
                return data;

            }
            else
            {
                return null;
            }

        }

        public Categories UpdatCategorye(int id, Categories category)
        {
            var ExixtingData = _context.Categories.Find(id);
            if (ExixtingData != null)
            {

 
                ExixtingData.CategoryName = category.CategoryName;
                _context.SaveChanges();
            return ExixtingData;
            }

            return null;

        }

        public void AddSubcategory(int categoryId, string subCategoryName)
         {
            try
            {
                var temp = new SubCategory() { CategoryId = categoryId, SubCategoryName = subCategoryName };

                _context.SubCategories.Add(temp);
                _context.SaveChanges();
            }
            catch(Exception ex)
            {}
        }
        //adding method for category update
        public void UpdateCategory(int Id, string CategoryName)
        {
            try
            {
                var temp = _context.Categories.Find(Id);
                if (temp == null)
                { throw new Exception("data not found"); }

                temp.CategoryName = CategoryName;
                _context.SaveChanges();
            }
            catch (Exception ex)
            { }
        }

        //adding method for subcategory update
        public void UpdateSubCategory(int Id, string SubCategoryName)
        {
            try
            {
                var temp = _context.SubCategories.Find(Id);
                if (temp == null)
                {
                    throw new Exception("data not found");
                }
                temp.SubCategoryName = SubCategoryName;
                _context.SaveChanges();
            }
            catch (Exception ex) { }
        }
    }
}
