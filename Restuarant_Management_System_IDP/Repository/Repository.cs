using Microsoft.EntityFrameworkCore;
using Restuarant_Management_System_IDP.Models;
using Restuarant_Management_System_IDP.Repository.IRepository;
using System.Linq.Expressions;

namespace Restuarant_Management_System_IDP.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly RestaurantDbContext _db;
        internal DbSet<T> dbSet;

        public Repository(RestaurantDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public T? Get(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = dbSet.Where(filter);
            return query.FirstOrDefault();
        }

        public List<T> GetAll()
        {
            return dbSet.ToList();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = dbSet.Where(filter);
            return query.ToList();
        }

        public T? Search(object id)
        {
            return dbSet.Find(id);
        }
    }
}
