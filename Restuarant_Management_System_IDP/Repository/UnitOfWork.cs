using Restuarant_Management_System_IDP.Models;
using Restuarant_Management_System_IDP.Repository.IRepository;

namespace Restuarant_Management_System_IDP.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private RestaurantDbContext _db;
        public IUserRepository User { get; private set; }

        public UnitOfWork(RestaurantDbContext db)
        {
            _db = db;
            User = new UserRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
