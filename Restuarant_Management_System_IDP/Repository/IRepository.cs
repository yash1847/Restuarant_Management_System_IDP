namespace Restuarant_Management_System_IDP.Repository
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll();
        bool Add(T item);
        bool Update(object id, T item);
        bool Delete(object id);
        T Search(object id);
    }
}
