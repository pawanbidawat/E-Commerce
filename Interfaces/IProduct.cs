using TemporalBoxApi.Models;

namespace TemporalBoxApi.Interfaces
{
    public interface IProduct
    {
        PagedProductData GetAllProducts(Paging paging);
        Product GetProduct(int id);
        List<Product> GetProductBySubCategoryId(int id);
        void AddProduct(Product product);
        void UpdateProduct(int id, Product product);
        bool DeleteProduct(int id);
        PagedProductData GetFilterProduct(Paging paging);
        PagedProductData pageFiltering(PagedProductData PagedProduct);
    }
}
