using Restuarant_Management_System_IDP.Models;

namespace Restuarant_Management_System_IDP.Repository.IRepository
{
    public interface ISubCategoryRepository : IRepository<SubCategory>
    {
        void Update(SubCategory obj);
    }
}
