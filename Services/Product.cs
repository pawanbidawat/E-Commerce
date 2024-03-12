using Azure.Messaging;
using TemporalBoxApi.Interfaces;
using TemporalBoxApi.JwtContext;
using TemporalBoxApi.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TemporalBoxApi.Services
{
    public class Product : IProduct
    {
        private readonly Context _context;
        public Product(Context context)
        {
            _context = context;
        }
        public void AddProduct(Models.Product product)
        {
            try
            {
                _context.Products.Add(product);
                _context.SaveChanges();
            }
            catch (Exception ex) { }


        }

        public Models.Product GetProduct(int id)
        {
            var data = _context.Products.FirstOrDefault(x => x.ProductId == id);
            return data;
        }

        //get Product by SubCategoryId
        public List<Models.Product> GetProductBySubCategoryId(int id)
        {
            var data  = _context.Products.Where(x=>x.SubCategoryId==id).ToList();
            return data;
        }


        //get all products according to page no.
        public PagedProductData GetAllProducts(Paging paging)
        {
            var data = _context.Products.ToList();
            var sortData = new List<Models.Product>();
            //adding sorting method 
            if (paging.ColumnName != null) {
                switch (paging.ColumnName)
                {
                    case "Product Name":
                        sortData = data.OrderBy(x => x.ProductName).ToList();
                        break;
                    case "Product Price":
                        sortData = data.OrderBy(x => x.ProductPrice).ToList();
                        break;                 

                    default:
                       
                        break;  
                }
                }


            //calling page filtering method
            PagedProductData pagedData = new()
            {
                PagedData = sortData.Any() ? sortData : data,
                Paging = paging
            };

            PagedProductData pagedResult = pageFiltering(pagedData);


            return pagedResult;
        }

        //page filtering method
        public PagedProductData pageFiltering(PagedProductData PagedProduct)
        {
            var paging = PagedProduct.Paging;
            var filteredProducts = PagedProduct.PagedData;
            int pageContent = 2;

            int pageCount = (int)Math.Ceiling(filteredProducts.Count() / (double)pageContent);
            int currentPage = paging.page != 0 ? paging.page : 1;

            var pagedData = filteredProducts.Skip((currentPage - 1) * pageContent).Take(pageContent).ToList();

            paging.currentPage = currentPage;
            paging.pageCount = pageCount;
            return new PagedProductData {
                PagedData = pagedData,
                Paging = paging
            };
        }

        public PagedProductData GetFilterProduct(Paging paging)
        {
            if (paging.min != 0 || paging.max != 0)
            {
                var filteredProducts = _context.Products.Where(x => x.ProductPrice >= paging.min && x.ProductPrice <= paging.max).ToList();

                //calling page method
                PagedProductData pagedData = new()
                {
                    PagedData = filteredProducts,
                    Paging = paging
                };
                PagedProductData pagedResult = pageFiltering(pagedData);

                //////////////////////

                return pagedResult;


            }
            else if(paging.page != 0 || paging.page == 0)
            {
                return GetAllProducts(paging);            
               

            }
         
            return new PagedProductData();
        }

        public bool DeleteProduct(int id)
        {
            var data = _context.Products.FirstOrDefault(x => x.ProductId == id);
            if (data != null)
            {
                _context.Products.Remove(data);
                _context.SaveChanges();
                return true;
            }
            return false;


        }

        void IProduct.UpdateProduct(int id, Models.Product product)
        {
            if (id == 0 || id == null)
            {
                id = product.ProductId;
                try
                {

                    if (id > 0)
                    {
                        var data = _context.Products.FirstOrDefault(x => x.ProductId == id);
                        data.ProductName = product.ProductName;
                        data.ProductPrice = product.ProductPrice;
                        data.ProductDescription = product.ProductDescription;
                        data.ProductImage = product.ProductImage;
                        _context.SaveChanges();

                    }

                }
                catch (Exception ex) { }
            }

        }

    }
}
