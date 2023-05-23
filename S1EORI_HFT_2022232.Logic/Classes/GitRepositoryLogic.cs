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
            var repoStats = from repo in repo.ReadAll()
                            let username = _userService.Read(repo.UserId).Username
                            let avgCommitLength = repo.Commits.Any() ? (int)repo.Commits.Average(c => c.Message.Length) : 0
                            select new RepositoryStatistics()
                            {
                                RepositoryName = repo.Name,
                                UserName = username,
                                CommitCount = repo.Commits.Count,
                                AverageCommitLength = avgCommitLength
                            };
            return repoStats.OrderByDescending(rs => rs.CommitCount);
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
