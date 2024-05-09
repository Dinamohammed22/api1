using api1.Models;
using api1.Repositories.RepoInterface;

namespace api1.Repositories
{
    public class ProductRepository : Repository<Product> , IProductRepository
    {
        public ProductRepository(StoreContext context) : base(context)
        {
        }

        public List<Product> GetProductsByCatId(int CategoryId)
        {
            return Context.Products.Where(p => p.CategoryId == CategoryId).ToList();
        }
        
        public List<string> GetProductNamessByCatId(int CategoryId)
        {
            return Context.Products.Where(p => p.CategoryId == CategoryId).Select(p => p.Name).ToList();
        }

    }
}
