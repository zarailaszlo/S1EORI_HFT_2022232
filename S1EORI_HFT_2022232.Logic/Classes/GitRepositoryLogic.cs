using S1EORI_HFT_2022232.Logic.Interfaces;
using S1EORI_HFT_2022232.Models;
using S1EORI_HFT_2022232.Models.Statistics;
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
        private readonly UserLogic _userService;
        public GitRepositoryLogic(IRepository<GitRepository> repo)
        {
            this.repo = repo;
        }

        public void Create(GitRepository item)
        {
            if (item.Visibility == "private" || item.Visibility == "public")
            {
                this.repo.Create(item);
            }
            else
            {
                throw new ArgumentException("Invalid Visibility (correct: private/public)");
            }
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
        public int GetCommitCountForRepository(int repositoryId)
        {
            var repository = repo.Read(repositoryId);
            if (repository == null)
            {
                throw new Exception($"No repository found with ID {repositoryId}");
            }
            return repository.Commits.Count;
        }
        public IQueryable<RepositoryStatistics> ReadRepositoryStats()
        {
            var repoStats = from rep in this.repo.ReadAll()
                            let userid = rep.UserId
                            let avgCommitLength = rep.Commits.Any() ? (int)rep.Commits.Average(c => c.Message.Length) : 0
                            select new RepositoryStatistics()
                            {
                                RepositoryName = rep.Name,
                                UserId = userid.ToString(),
                                CommitCount = rep.Commits.Count,
                                AverageCommitLength = avgCommitLength
                            };
            return repoStats;
        }
        public IQueryable<VisibilityGroupStatistics> GroupRepositoriesByVisibility()
        {
            var visibilityGroups = from repository in repo.ReadAll()
                                   group repository by repository.Visibility into visibilityGroup
                                   select new VisibilityGroupStatistics()
                                   {
                                       Visibility = visibilityGroup.Key,
                                       RepositoryCount = visibilityGroup.Count()
                                   };
            return visibilityGroups;
        }
    }
}
