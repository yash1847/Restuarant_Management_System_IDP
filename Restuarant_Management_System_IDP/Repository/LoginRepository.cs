using Restuarant_Management_System_IDP.Models;
using Restuarant_Management_System_IDP.Repository.IRepository;

namespace Restuarant_Management_System_IDP.Repository
{
    public class LoginRepository : Repository<Login>, ILoginRepository
    {
        private RestaurantDbContext _db;

        public LoginRepository(RestaurantDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Login entity)
        {
            _db.Logins.Update(entity);   
        }
    }
}
