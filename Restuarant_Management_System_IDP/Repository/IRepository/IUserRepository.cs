using Restuarant_Management_System_IDP.Models;

namespace Restuarant_Management_System_IDP.Repository.IRepository
{
    public interface IUserRepository : IRepository<Usertb>
    {
        void Update(Usertb obj);
    }
}
