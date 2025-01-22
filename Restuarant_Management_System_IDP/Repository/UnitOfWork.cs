using Restuarant_Management_System_IDP.Data;
using Restuarant_Management_System_IDP.Repository.IRepository;

namespace Restuarant_Management_System_IDP.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private RestaurantDbContext _db;
        public IUserRepository User { get; private set; }

        public IAddresstbRepository Addresstb { get; private set; }

        public ICategoryRepository Category { get; private set; }

        public ISubCategoryRepository SubCategory { get; private set; }

        public IMenuItemRepository MenuItem { get; private set; }

        public IOrderDetailRepository OrderDetail { get; private set; }

        public IOrderHeaderRepository OrderHeader { get; private set; }

        public IShoppingCartRepository ShoppingCart { get; private set; }

        public UnitOfWork(RestaurantDbContext db)
        {
            _db = db;
            User = new UserRepository(_db);
            Addresstb = new AddresstbRepository(_db);
            Category = new CategoryRepository(_db);
            SubCategory = new SubCategoryRepository(_db);
            MenuItem = new MenuItemRepository(_db);
            OrderDetail = new OrderDetailRepository(_db);
            OrderHeader = new OrderHeaderRepository(_db);
            ShoppingCart = new ShoppingCartRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
