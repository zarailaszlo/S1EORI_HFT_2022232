using S1EORI_HFT_2022232.Logic.Interfaces;
using S1EORI_HFT_2022232.Models;
using S1EORI_HFT_2022232.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S1EORI_HFT_2022232.Logic.Classes
{
    public class GitRepositoryLogic : IGitRepositoryLogic
    {
        IRepository<GitRepository> repo;
        public GitRepositoryLogic(IRepository<GitRepository> repo)
        {
            this.repo = repo;
        }

        public void Create(GitRepository item)
        {
            if (item.Name == null)
            {
                throw new ArgumentException("Missing Name");
            }
            if (item.Visibility != "private" || item.Visibility != "public")
            {
                throw new ArgumentException("Invalid Visibility");
            }
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public GitRepository Read(int id)
        {
            return this.repo.Read(id);
        }

        public IQueryable<GitRepository> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(GitRepository item)
        {
            this.repo.Update(item);
        }
    }
}
