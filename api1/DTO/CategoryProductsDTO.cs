using api1.Models;

namespace api1.DTO
{
    public class CategoryProductsDTO
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<string> ProductNames { get; set; }
    }
}
