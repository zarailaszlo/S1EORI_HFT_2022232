using S1EORI_HFT_2022232.Models;
using System;
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
            var old = Read(item.IdUser);
            foreach (var prop in old.GetType().GetProperties())
            {
                prop.SetValue(old, prop.GetValue(item));
            }
            _context.SaveChanges();
        }
    }
}
