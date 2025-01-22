using Restuarant_Management_System_IDP.Models;

namespace Restuarant_Management_System_IDP.Repository.IRepository
{
    public interface IOrderDetailRepository:IRepository<OrderDetail>
    {
        void Update(OrderDetail orderDetail);
    }
}
