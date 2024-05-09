using api1.Models;

namespace api1.Repositories.RepoInterface
{
    public interface IProductRepository : IRepository<Product>
    {
        public List<Product> GetProductsByCatId(int CategoryId);
        public List<string> GetProductNamessByCatId(int CategoryId);
    }
}
