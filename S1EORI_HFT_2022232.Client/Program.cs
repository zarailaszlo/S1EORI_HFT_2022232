using ConsoleTools;
using S1EORI_HFT_2022232.Models;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace S1EORI_HFT_2022232.Client
{
    internal class Program
    {
        static RestService rest;
        static void Create(string entity)
        {
            
        }
        static void List(string entity)
        {
            
        }
        static void Update(string entity)
        {
            
        }
        static void Delete(string entity)
        {
            
        }
        static void Main(string[] args)
        {
            rest = new RestService("http://localhost:58989/", "git");

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

            var menu = new ConsoleMenu(args, level: 0)
                .Add("Users", () => userSubMenu.Show())
                .Add("GitRepositories", () => gitRepositorySubMenu.Show())
                .Add("Commits", () => commitSubMenu.Show())
                .Add("Exit", ConsoleMenu.Close);
           
            menu.Show();
        }
    }
}
