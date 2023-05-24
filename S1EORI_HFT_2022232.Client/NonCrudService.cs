using S1EORI_HFT_2022232.Models;
using S1EORI_HFT_2022232.Models.Statistics;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S1EORI_HFT_2022232.Client
{
    public class NonCrudService
    {
        RestService rest;
         
        public NonCrudService(RestService restService)
        {
            rest = restService;
        }

        public void GetCommitsNewerThan()
        {

        }
        public void ReadCommitsLongerThan()
        {

        }
        public void GetCommitCountForRepository()
        {

        }
        public void ReadRepositoryStats()
        {
            var items = rest.Get<RepositoryStatistics>("Stat/ReadRepositoryStats");
            foreach (var item in items)
            {
                Console.WriteLine(item);
            }
            Console.ReadLine();
        }
        public void GroupRepositoriesByVisibility()
        {
            var items = rest.Get<VisibilityGroupStatistics>("Stat/GroupRepositoriesByVisibility");
            foreach (var item in items)
            {
                Console.WriteLine(item);
            }
            Console.ReadLine();
        }
        public void ReadUsersWithZeroRepositories()
        {
            Console.WriteLine("User(s) With Zero Repositories");
            var items = rest.Get<User>($"Stat/ReadUsersWithZeroRepositories");
            foreach (var item in items)
            {
                Console.WriteLine(item);
            }
            Console.ReadLine();
        }
        public void DoesUserWithEmailExist()
        {

        }
        public void ReadUsersOlderThan()
        {

        }
    }
}
//IQueryable<Commit> GetCommitsNewerThan(DateTime date);
//IQueryable<Commit> ReadCommitsLongerThan(int length);
//int GetCommitCountForRepository(int repositoryId);
//IQueryable<RepositoryStatistics> ReadRepositoryStats();
//IQueryable<VisibilityGroupStatistics> GroupRepositoriesByVisibility();
//IQueryable<User> ReadUsersWithZeroRepositories();
//bool DoesUserWithEmailExist(string email);
//IQueryable<User> ReadUsersOlderThan(int age);