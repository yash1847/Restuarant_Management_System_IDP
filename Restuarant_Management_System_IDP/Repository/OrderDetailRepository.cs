using Restuarant_Management_System_IDP.Data;
using Restuarant_Management_System_IDP.Models;
using Restuarant_Management_System_IDP.Repository.IRepository;

namespace Restuarant_Management_System_IDP.Repository
{
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        private readonly RestaurantDbContext _db;
        public OrderDetailRepository(RestaurantDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(OrderDetail orderDetail)
        {
            throw new NotImplementedException();
        }
    }
}
