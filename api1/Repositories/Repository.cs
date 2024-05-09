using api1.Models;
using api1.Repositories.RepoInterface;
using Microsoft.EntityFrameworkCore;

namespace api1.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public StoreContext Context { get; }

        public Repository(StoreContext context)
        {
            Context = context;
        }

        public List<T> GetAll()
        {
            return Context.Set<T>().ToList();
        }
        public T Get(int id)
        {
            return Context.Set<T>().Find(id);
        }
       
        public void insert(T entity)
        {
            Context.Set<T>().Add(entity);

        }

        public bool Delete(int id)
        {
            T entity = Get(id);
            if (entity == null)
            {
                return false;
            }
            Context.Set<T>().Remove(entity);

            return true;
        }
  


        public bool Update(int id , T UpdatedEntity)
        {
            //
            T entity = Get(id);
            //if(entity == null)
            //{
            //    return false;
            //}
            Context.Set<T>().Update(UpdatedEntity);
            return true;
        }

        public void save()
        {
            Context.SaveChanges();
        }
    }

    
}
