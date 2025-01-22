using Restuarant_Management_System_IDP.Data;
using Restuarant_Management_System_IDP.Models;
using Restuarant_Management_System_IDP.Repository.IRepository;

namespace Restuarant_Management_System_IDP.Repository
{
    public class ShoppingCartRepository:Repository<ShoppingCart>, IShoppingCartRepository
    {
        private readonly RestaurantDbContext _db;

        public ShoppingCartRepository(RestaurantDbContext db):base(db)
        {
            _db = db;
        }

        public void Update(ShoppingCart obj)
        {
            throw new NotImplementedException();
        }
    }
}
