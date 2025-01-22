using Restuarant_Management_System_IDP.Models;

namespace Restuarant_Management_System_IDP.Repository.IRepository
{
    public interface IMenuItemRepository : IRepository<MenuItem>
    {
        void Update(MenuItem obj);
    }
}
