using api1.Models;
using api1.Repositories.RepoInterface;

namespace api1.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(StoreContext context) : base(context)
        {
        }


        public string GetName(int id)
        {
            return Context.Categories.Where(c => c.Id == id).First().Name;
        }

    }
}
