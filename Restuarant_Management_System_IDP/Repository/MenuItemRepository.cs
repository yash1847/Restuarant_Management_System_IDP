using Restuarant_Management_System_IDP.Data;
using Restuarant_Management_System_IDP.Models;
using Restuarant_Management_System_IDP.Repository.IRepository;

namespace Restuarant_Management_System_IDP.Repository
{
    public class MenuItemRepository : Repository<MenuItem>, IMenuItemRepository
    {
        private readonly RestaurantDbContext _db;

        public MenuItemRepository(RestaurantDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(MenuItem obj)
        {
            throw new NotImplementedException();
        }
    }
}
