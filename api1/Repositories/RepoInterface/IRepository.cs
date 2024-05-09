using Microsoft.AspNetCore.Mvc;

namespace api1.Repositories.RepoInterface
{
    public interface IRepository<T> where T : class
    {
        public List<T> GetAll();
        public T Get(int id);

        public void insert(T entity);

        public bool Delete(int id);

        public bool Update(int id , T entity);

        public void save(); 
    }
}
