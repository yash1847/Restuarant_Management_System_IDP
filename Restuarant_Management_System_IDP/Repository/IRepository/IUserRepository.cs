using Restuarant_Management_System_IDP.Models;

namespace Restuarant_Management_System_IDP.Repository.IRepository
{
    public interface IUserRepository : IRepository<User>
    {
        void Update(User obj);
    }
}
