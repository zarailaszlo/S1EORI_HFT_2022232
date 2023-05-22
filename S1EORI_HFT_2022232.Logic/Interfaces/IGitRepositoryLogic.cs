using S1EORI_HFT_2022232.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S1EORI_HFT_2022232.Logic.Interfaces
{
    public interface IGitRepositoryLogic
    {
        void Create(GitRepository item);
        void Delete(int id);
        GitRepository Read(int id);
        IQueryable<GitRepository> ReadAll();
        void Update(GitRepository item);
        int GetCommitCountForRepository(int repositoryId);
    }
}
