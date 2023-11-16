using S1EORI_HFT_2022232.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S1EORI_HFT_2022232.Repository.ModelsRepository
{
    public class UserRepository : MainRepository<User>
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public override User Read(int id)
        {
            return _context.Users.FirstOrDefault(e => e.IdUser == id);

        }

        public override void Update(User item)
        {
            User old = Read(item.IdUser);

            var properties = typeof(User)
                .GetProperties()
                .Where(p => p.CanWrite && !typeof(ICollection)
                .IsAssignableFrom(p.PropertyType));

            foreach (var prop in properties)
            {
                var newValue = prop.GetValue(item, null);
                prop.SetValue(old, newValue, null);
            }

            _context.SaveChanges();
        }
        //Erre hibát dob
        //public override void Update(User item)
        //{
        //    User old = Read(item.IdUser);
        //    foreach (var prop in old.GetType().GetProperties())
        //    {
        //        prop.SetValue(old, prop.GetValue(item));
        //    }
        //    _context.SaveChanges();
        //}

    }
}
