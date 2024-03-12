using TemporalBoxApi.Models;

namespace TemporalBoxApi.Interfaces
{
    public interface ICategory
    {
        List<Categories> GetCategorieDetails();
        List<SubCategory> GetSubCategory(int id);
        List<SubCategory> GetSubCategoryWithId(int id);
        Categories GetCategory(int id);
        Categories AddCategory(string category);
        Categories UpdatCategorye(int id, Categories category);
        bool DeleteCategory(int id);
        bool DeleteSubCategory(int id);
        void AddSubcategory(int categoryId, string subCategoryName);

        void UpdateCategory(int id, string categoryName);
        public void UpdateSubCategory(int Id, string SubCategoryName);

    }
}
