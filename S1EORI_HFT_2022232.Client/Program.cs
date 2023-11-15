using ConsoleTools;
using S1EORI_HFT_2022232.Models;
using S1EORI_HFT_2022232.Models.Statistics;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Xml.Linq;

namespace S1EORI_HFT_2022232.Client
{
    internal class Program
    {
        static RestService rest;
        //CrudService
        static void Create(string entity)
        {
            if (entity == "User")
            {
                Console.Write("Username: ");
                string username = Console.ReadLine();
                Console.Write("Password: ");
                string password = Console.ReadLine();
                Console.Write("FullName: ");
                string fullName = Console.ReadLine();
                Console.Write("Email: ");
                string email = Console.ReadLine();
                Console.Write("Age: ");
                int age = int.Parse(Console.ReadLine());
                rest.Post(new User() { Username = username, Password = password, FullName = fullName, Email = email, Age = age }, "user");
            }
            else if (entity == "GitRepository")
            {
                Console.Write("Repository name: ");
                string name = Console.ReadLine();
                Console.Write("Visibility (private/public): ");
                string vis = Console.ReadLine();
                string visibility;
                if (vis == null || vis != "private" || vis != "public")
                {
                    visibility = "private";
                }
                visibility = vis;
                Console.WriteLine("Repository Created Date set");
                DateTime createdDate = DateTime.Now;
                Console.Write("Repository Owner ID: ");
                int userId = int.Parse(Console.ReadLine());
                rest.Post(new GitRepository { Name = name, Visibility = visibility, CreatedDate = createdDate, UserId = userId }, "gitRepository");
            }
            else if (entity == "Commit")
            {
                Console.Write("Commit hash: ");
                string hash = Console.ReadLine();

                Console.Write("Commit message: ");
                string message = Console.ReadLine();
                Console.WriteLine("Created Date set");
                DateTime committedDate = DateTime.Now;
                Console.Write("Commit RepositoryId: ");
                int gitRepositoryId = int.Parse(Console.ReadLine());
                Console.Write("Commit UserId: ");
                int userId = int.Parse(Console.ReadLine());
                rest.Post(new Commit {Hash = hash, Message = message, CommittedDate = committedDate, GitRepositoryId = gitRepositoryId, UserId = userId }, "commit");
            }
        }
        static void List(string entity)
        {
            if (entity == "User")
            {
                List<User> users = rest.Get<User>("User");
                foreach (var item in users)
                {
                    Console.WriteLine(item);
                }
            }
            else if (entity == "GitRepository")
            {
                List<GitRepository> gitRepository = rest.Get<GitRepository>("GitRepository");
                foreach (var item in gitRepository)
                {
                    Console.WriteLine(item);
                }
            }
            else if (entity == "Commit")
            {
                List<Commit> commits = rest.Get<Commit>("Commit");
                foreach (var item in commits)
                {
                    Console.WriteLine(item);
                }
            }
            Console.ReadLine();
        }
        static void Update(string entity)
        {
            if (entity == "User")
            {
                Console.Write("Enter User's id to update: ");
                int id = int.Parse(Console.ReadLine());
                User one = rest.Get<User>(id, "User");
                Console.Write($"New name [old: {one.Username}]: ");
                string name = Console.ReadLine();
                one.Username = name;                
                rest.Put(one, "User");
            }
            else if (entity == "GitRepository")
            {
                Console.Write("Enter GitRepository's id to update: ");
                int id = int.Parse(Console.ReadLine());
                GitRepository one = rest.Get<GitRepository>(id, "GitRepository");
                Console.Write($"New name [old: {one.Name}]: ");
                string name = Console.ReadLine();
                one.Name = name;
                rest.Put(one, "GitRepository");
            }
            else if (entity == "Commit")
            {
                Console.Write("Enter Commit's id to update: ");
                int id = int.Parse(Console.ReadLine());
                Commit one = rest.Get<Commit>(id, "Commit");
                Console.Write($"New Message [old: {one.Message}]: ");
                string message = Console.ReadLine();
                one.Message = message;
                rest.Put(one, "Commit");
            }
        }
        static void Delete(string entity)
        {
            if (entity == "User")
            {
                Console.Write("Enter User's id to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "user");
            }
            else if (entity == "GitRepository")
            {
                Console.Write("Enter GitRepository's id to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "gitRepository");
            }
            else if (entity == "Commit")
            {
                Console.Write("Enter Commit's id to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "commit");
            }
        }
        static void Main(string[] args)
        {
            rest = new RestService("http://localhost:58988/", "User");
            NonCrudService nonCrud = new NonCrudService(rest);

            var userSubMenu = new ConsoleMenu(args, level:1)
                .Add("List", () => List("User"))
                .Add("Create", () => Create("User"))
                .Add("Delete", () => Delete("User"))
                .Add("Update", () => Update("User"))
                .Add("Exit", ConsoleMenu.Close);

            var gitRepositorySubMenu = new ConsoleMenu(args, level:1)
                .Add("List", () => List("GitRepository"))
                .Add("Create", () => Create("GitRepository"))
                .Add("Delete", () => Delete("GitRepository"))
                .Add("Update", () => Update("GitRepository"))
                .Add("Exit", ConsoleMenu.Close);

            var commitSubMenu = new ConsoleMenu(args, level:1)
                .Add("List", () => List("Commit"))
                .Add("Create", () => Create("Commit"))
                .Add("Delete", () => Delete("Commit"))
                .Add("Update", () => Update("Commit"))
                .Add("Exit", ConsoleMenu.Close);
            
            var nonCRUDSubMenu = new ConsoleMenu(args, level: 1)
                .Add("Repository Commit Count", () => nonCrud.GetCommitCountForRepository())
                .Add("Repository Stats", () => nonCrud.ReadRepositoryStats())
                .Add("Visibility Stats", () => nonCrud.GroupRepositoriesByVisibility())
                .Add("Users With Zero Repositories", () => nonCrud.ReadUsersWithZeroRepositories())
                .Add("Users Older Than", () => nonCrud.ReadUsersOlderThan())
                .Add("Exit", ConsoleMenu.Close);


            var menu = new ConsoleMenu(args, level: 0)
                .Add("Users", () => userSubMenu.Show())
                .Add("GitRepositories", () => gitRepositorySubMenu.Show())
                .Add("Commits", () => commitSubMenu.Show())
                .Add("Non-CRUD", () => nonCRUDSubMenu.Show())
                .Add("Exit", ConsoleMenu.Close);
           
            menu.Show();
        }
    }
}
