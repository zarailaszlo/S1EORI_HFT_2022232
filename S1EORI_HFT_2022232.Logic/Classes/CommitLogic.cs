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
    public class CommitLogic : ICommitLogic
    {
        IRepository<Commit> repo;
        public CommitLogic(IRepository<Commit> repo)
        {
            this.repo = repo;
        }

        public void Create(Commit item)
        {
            if (item.Hash == null)
            {
                throw new ArgumentException("Missing Hash");
            }
            if (item.Message == null)
            {
                throw new ArgumentException("Missing Message");
            }            
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Commit Read(int id)
        {
            return this.repo.Read(id);
        }

        public IQueryable<Commit> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Commit item)
        {
            this.repo.Update(item);
        }
    }
}
