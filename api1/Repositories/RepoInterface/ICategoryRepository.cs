using api1.Models;

namespace api1.Repositories.RepoInterface
{
    public interface ICategoryRepository:IRepository<Category>
    {
        public string GetName(int id);
    }
}
