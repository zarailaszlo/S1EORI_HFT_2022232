using S1EORI_HFT_2022232.Models;
using S1EORI_HFT_2022232.Models.Statistics;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.ConstrainedExecution;
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
        public void GetCommitCountForRepository()
        {
            Console.Write("RepositoryID: ");
            int id = int.Parse(Console.ReadLine());
            var item = rest.GetSingle<double>($"Stat/GetCommitCountForRepository?repositoryId={id}");
            Console.WriteLine("Commit Count: "+item);
            Console.ReadLine();

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
        public void ReadUsersOlderThan()
        {
            Console.Write("Age: ");
            int age = int.Parse(Console.ReadLine());
            var items = rest.Get<User>($"Stat/ReadUsersOlderThan?age={age}");
            foreach(var item in items)
            {
                Console.WriteLine(item);
            }
            Console.ReadLine();
        }
    }
}