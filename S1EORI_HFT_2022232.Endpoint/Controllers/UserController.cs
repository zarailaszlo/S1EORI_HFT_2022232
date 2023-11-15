using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using S1EORI_HFT_2022232.Endpoint.Services;
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
        IHubContext<SignalRHub> hub;

        public UserController(IUserLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
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
            this.hub.Clients.All.SendAsync("UserCreated", value);
        }

        [HttpPut]
        public void Update([FromBody] User value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("UserUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var userToDelete = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("UserDeleted", userToDelete);
        }
    }
}
