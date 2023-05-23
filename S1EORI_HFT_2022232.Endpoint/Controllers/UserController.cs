using Microsoft.AspNetCore.Mvc;
using S1EORI_HFT_2022232.Logic.Interfaces;
using S1EORI_HFT_2022232.Models;
using System.Collections.Generic;

namespace S1EORI_HFT_2022232.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserLogic logic;

        public UserController(IUserLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet]
        public IEnumerable<User> ReadAll()
        {
            return this.logic.ReadAll();
        }

        [HttpGet("{id}")]
        public User Read(int id)
        {
            return this.logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] User value)
        {
            this.logic.Create(value);
        }

        [HttpPut]
        public void Update([FromBody] User value)
        {
            this.logic.Update(value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.logic.Delete(id);
        }
    }
}
