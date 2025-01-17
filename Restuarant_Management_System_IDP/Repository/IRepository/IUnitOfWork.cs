namespace Restuarant_Management_System_IDP.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IUserRepository User { get; }
        void Save();
    }
}
