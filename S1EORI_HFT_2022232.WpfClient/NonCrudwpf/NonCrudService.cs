using S1EORI_HFT_2022232.Models.Statistics;
using S1EORI_HFT_2022232.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S1EORI_HFT_2022232.WpfClient.NonCrudwpf
{
    class NonCrudService
    {
        RestService rest;

        public NonCrudService(RestService restService)
        {
            rest = restService;
        }

        public double GetCommitCountForRepository(int repositoryId)
        {
            return rest.GetSingle<double>($"Stat/GetCommitCountForRepository?repositoryId={repositoryId}");
        }

        public IEnumerable<RepositoryStatistics> ReadRepositoryStats()
        {
            return rest.Get<RepositoryStatistics>("Stat/ReadRepositoryStats");
        }

        public IEnumerable<VisibilityGroupStatistics> GroupRepositoriesByVisibility()
        {
            return rest.Get<VisibilityGroupStatistics>("Stat/GroupRepositoriesByVisibility");
        }

        public IEnumerable<User> ReadUsersWithZeroRepositories()
        {
            return rest.Get<User>($"Stat/ReadUsersWithZeroRepositories");
        }

        public IEnumerable<User> ReadUsersOlderThan(int age)
        {
            return rest.Get<User>($"Stat/ReadUsersOlderThan?age={age}");
        }
    }
}