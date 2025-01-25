using Microsoft.EntityFrameworkCore;
using Restuarant_Management_System_IDP.Data;
using Restuarant_Management_System_IDP.Models;
using Restuarant_Management_System_IDP.Repository.IRepository;

namespace Restuarant_Management_System_IDP.Repository
{
    public class SubCategoryRepository : Repository<SubCategory>, ISubCategoryRepository
    {
        private readonly RestaurantDbContext _db;

        public SubCategoryRepository(RestaurantDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(SubCategory obj)
        {
            //SubCategory subCategory = Search(obj.Id);
            ////_db.SubCategories.Find(obj.Id);
            ////subCategory = Search(obj.Id);
            //subCategory.Name = obj.Name;
            _db.SubCategories.Update(obj);
        }
    }
}
