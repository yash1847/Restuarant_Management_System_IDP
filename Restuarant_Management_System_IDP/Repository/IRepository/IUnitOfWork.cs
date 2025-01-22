namespace Restuarant_Management_System_IDP.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IUserRepository User { get; }
        IAddresstbRepository Addresstb { get; }
        ICategoryRepository Category { get; }
        ISubCategoryRepository SubCategory { get; }
        IMenuItemRepository MenuItem { get; }
        IOrderDetailRepository OrderDetail { get; }
        IOrderHeaderRepository OrderHeader { get; }
        IShoppingCartRepository ShoppingCart { get; }

        void Save();
    }
}
