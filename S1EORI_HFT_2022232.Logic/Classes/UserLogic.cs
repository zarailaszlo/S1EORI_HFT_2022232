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
    public class UserLogic : IUserLogic
    {
        IRepository<User> repo;
        public UserLogic(IRepository<User> repo)
        {
            this.repo = repo;
        }

        public void Create(User item)
        {
            if (item.Username.Length < 2) 
            {
                throw new ArgumentException("The Username is too short");
            }
            if (item.Password.Length < 8) 
            {
                throw new ArgumentException("The Password is too short");
            }
            if (!item.Email.Contains("@"))
            {
                throw new ArgumentException("Bad email address (missing @)");
            }
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public User Read(int id)
        {
            return this.repo.Read(id);
        }

        public IQueryable<User> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(User item)
        {
            this.repo.Update(item);
        }
        public IQueryable<User> ReadUsersWithZeroRepositories()
        {
            var usersWithZeroRepositories = from user in repo.ReadAll()
                                            where user.GitRepositories.Count == 0
                                            select user;
            return usersWithZeroRepositories;
        }
        public IQueryable<User> ReadUsersOlderThan(int age)
        {
            var olderUsers = from user in repo.ReadAll()
                             where user.Age > age
                             select user;
            return olderUsers;
        }
    }
}
