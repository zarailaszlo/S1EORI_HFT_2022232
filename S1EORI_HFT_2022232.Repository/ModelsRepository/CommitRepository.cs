using S1EORI_HFT_2022232.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S1EORI_HFT_2022232.Repository.ModelsRepository
{
    public class CommitRepository : MainRepository<Commit>
    {
        public CommitRepository(AppDbContext context) : base(context)
        {
        }

        public override Commit Read(int id)
        {
            return _context.Commits.FirstOrDefault(x => x.IdCommit == id);
        }

        
        public override void Update(Commit item)
        {
            Commit old = Read(item.IdCommit);
            var properties = typeof(Commit)
                .GetProperties()
                .Where(p => p.CanWrite && !typeof(ICollection)
                .IsAssignableFrom(p.PropertyType)
            );

            foreach (var prop in properties)
            {
                var newValue = prop.GetValue(item, null);
                prop.SetValue(old, newValue, null);
            }

            _context.SaveChanges();
        }
        //public override void Update(Commit item)
        //{
        //    Commit old = Read(item.IdCommit);
        //    foreach (var prop in old.GetType().GetProperties())
        //    {
        //        prop.SetValue(old, prop.GetValue(item));
        //    }
        //    _context.SaveChanges();
        //}
    }
}
