using S1EORI_HFT_2022232.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S1EORI_HFT_2022232.Logic.Interfaces
{
    public interface ICommitLogic
    {
        void Create(Commit item);
        void Delete(int id);
        Commit Read(int id);
        IQueryable<Commit> ReadAll();
        void Update(Commit item);
        IQueryable<Commit> GetCommitsNewerThan(DateTime date);
    }
}
