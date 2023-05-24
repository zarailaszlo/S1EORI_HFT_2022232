using S1EORI_HFT_2022232.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S1EORI_HFT_2022232.Logic.Interfaces
{
    public interface IUserLogic
    {
        void Create(User item);
        void Delete(int id);
        User Read(int id);
        IQueryable<User> ReadAll();
        void Update(User item);
    }
}
