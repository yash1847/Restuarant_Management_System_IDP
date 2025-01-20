using Restuarant_Management_System_IDP.Models;

namespace Restuarant_Management_System_IDP.Repository.IRepository
{
    public interface IAddresstbRepository : IRepository<Addresstb>
    {
        void Update(Addresstb obj);
    }
}
