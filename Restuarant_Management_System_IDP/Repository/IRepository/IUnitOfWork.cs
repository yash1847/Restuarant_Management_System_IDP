namespace Restuarant_Management_System_IDP.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IUserRepository User { get; }
        ILoginRepository Login { get; }

        void Save();
    }
}
