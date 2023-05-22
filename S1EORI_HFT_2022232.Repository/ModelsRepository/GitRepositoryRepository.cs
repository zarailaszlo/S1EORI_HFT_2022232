using S1EORI_HFT_2022232.Models;
using System;
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
            var old = Read(item.IdGitRepository);
            foreach (var prop in old.GetType().GetProperties())
            {
                prop.SetValue(old, prop.GetValue(item));
            }
            _context.SaveChanges();
        }
    }
}
