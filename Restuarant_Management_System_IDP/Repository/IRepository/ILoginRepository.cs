using Restuarant_Management_System_IDP.Models;

namespace Restuarant_Management_System_IDP.Repository.IRepository
{
    public interface ILoginRepository : IRepository<Login>
    {
        void Update(Login entity);

    }
}
