using Restuarant_Management_System_IDP.Data;
using Restuarant_Management_System_IDP.Models;
using Restuarant_Management_System_IDP.Repository.IRepository;

namespace Restuarant_Management_System_IDP.Repository
{
    public class AddresstbRepository : Repository<Addresstb>, IAddresstbRepository
    {
        private readonly RestaurantDbContext _db;

        public AddresstbRepository(RestaurantDbContext db):base(db)
        {
            _db = db;
        }

        public void Update(Addresstb obj)
        {
            _db.Addressess.Update(obj);
        }
    }
}
