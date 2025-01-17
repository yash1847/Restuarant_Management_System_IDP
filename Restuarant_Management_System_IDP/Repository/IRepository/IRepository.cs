using System.Linq.Expressions;

namespace Restuarant_Management_System_IDP.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll();
        void Add(T entity);
        void Delete(T entity);
        T? Search(object id);
        T? Get(Expression<Func<T, bool>> filter);
        IEnumerable<T> GetAll(Expression<Func<T, bool>> filter);
    }
}
