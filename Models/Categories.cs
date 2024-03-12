using System.ComponentModel.DataAnnotations;

namespace TemporalBoxApi.Models
{
    public class Categories
    {
        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<SubCategory> SubCategoryList { get; set; }
    }
}
