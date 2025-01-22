using Restuarant_Management_System_IDP.Data;
using Restuarant_Management_System_IDP.Models;
using Restuarant_Management_System_IDP.Repository.IRepository;
using System.Diagnostics;

namespace Restuarant_Management_System_IDP.Repository
{
    public class UserRepository : Repository<Usertb>, IUserRepository
        //: IRepository<User>
    {
        public RestaurantDbContext _db;

        public UserRepository(RestaurantDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Usertb obj)
        {
            _db.Userstb.Update(obj);
        }

        /*
        public UserRepository(RestaurantDbContext context)
        {
            this.context = context;
        }

        public bool Add(User item)
        {
            try
            {
                context.Users.Add(item);
                return context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("##### ",ex.Message);
                return false;
            }
        }

        public bool Delete(object id)
        {
            var user = context.Users.Find(id);
            if (user != null)
            {
                context.Users.Remove(user);
                return context.SaveChanges() > 0;
            }
            return false;
        }

        public List<User> GetAll()
        {
            return context.Users.ToList();
        }

        public User Search(object id)
        {
            return context.Users.Find(id);
        }

        public bool Update(object id, User item)
        {
            User user = context.Users.Find(id);
            if (user != null)
            {
                user.FirstName = item.FirstName;
                user.LastName = item.LastName;
                user.Email = item.Email;
                user.Contact = item.Contact;
                user.Contact2 = item.Contact2;
                return context.SaveChanges() > 0;
            }
            return false;
        }
        */
    }
}
