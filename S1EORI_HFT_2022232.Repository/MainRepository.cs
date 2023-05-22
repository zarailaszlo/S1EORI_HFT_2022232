using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S1EORI_HFT_2022232.Repository
{
    public abstract class MainRepository<T> : IRepository<T> where T : class
    {
        protected AppDbContext _context;
        protected MainRepository(AppDbContext context)
        {
            this._context = context;
        }
        public void Create(T item)
        {
            _context.Set<T>().Add(item);
            _context.SaveChanges();
        }
        public IQueryable<T> ReadAll()
        {
            return _context.Set<T>();
        }
        public void Delete(int id)
        {
            _context.Set<T>().Remove(Read(id));
            _context.SaveChanges();
        }
        public abstract T Read(int id);
        public abstract void Update(T item);
    }
}
