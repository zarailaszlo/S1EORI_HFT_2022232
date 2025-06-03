using S1EORI_HFT_2022232.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S1EORI_HFT_2022232.Repository.ModelsRepository
{
    public class GitRepositoryRepository : MainRepository<GitRepository>
    {
        public GitRepositoryRepository(AppDbContext context) : base(context)
        {
        }

        public override GitRepository Read(int id)
        {
            return _context.GitRepositorys.FirstOrDefault(e => e.IdGitRepository == id);

        }
        public override void Update(GitRepository item)
        {
            GitRepository old = Read(item.IdGitRepository);

            var itemType = item.GetType();
            foreach (var prop in typeof(GitRepository).GetProperties())
            {
                var itemProp = itemType.GetProperty(prop.Name);
                if (itemProp != null)
                {
                    var newValue = itemProp.GetValue(item);
                    prop.SetValue(old, newValue);
                }
            }

            _context.SaveChanges();
        }
        // This version does not work
        //public override void Update(GitRepository item)
        //{
        //    GitRepository old = Read(item.IdGitRepository);
        //    foreach (var prop in old.GetType().GetProperties())
        //    {
        //        prop.SetValue(old, prop.GetValue(item));
        //    }
        //    _context.SaveChanges();
        //}        
    }
}
