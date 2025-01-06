using Restuarant_Management_System_IDP.Models;
using System.Diagnostics;

namespace Restuarant_Management_System_IDP.Repository
{
    public class LoginRepository : IRepository<Login>
    {
        public RestaurantDbContext context;

        public LoginRepository(RestaurantDbContext _context) //Dependency Injection
        {
            context = _context;
        }

        public bool Add(Login item)
        {
            try
            {
                context.Logins.Add(item);
                return context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("#####", ex.Message);
                return false;
            }
        }

        public bool Delete(object id)
        {
            var login = context.Logins.Find(id);
            if (login != null)
            {
                context.Logins.Remove(login);
                return context.SaveChanges() > 0;
            }
            return false;
        }

        public Login Search(object id)
        {
            return context.Logins.Find(id);
        }

        public List<Login> GetAll()
        {
            return context.Logins.ToList();
        }

        public bool Update(object id, Login item)
        {
            Login login = context.Logins.Find(id);
            if (login != null)
            {
                //login.Email = item.Email;
                login.Password = item.Password;
                return context.SaveChanges() > 0;
            }
            return false; //user not found
        }
    }
}
