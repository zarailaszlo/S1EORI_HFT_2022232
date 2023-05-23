using Microsoft.AspNetCore.Mvc;
using S1EORI_HFT_2022232.Logic.Interfaces;
using S1EORI_HFT_2022232.Models;
using System.Collections.Generic;


namespace S1EORI_HFT_2022232.Endpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommitController : ControllerBase
    {
        ICommitLogic logic;
        
        public CommitController(ICommitLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet]
        public IEnumerable<Commit> ReadAll()
        {
            return this.logic.ReadAll();
        }

        [HttpGet("{id}")]
        public Commit Read(int id)
        {
            return this.logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Commit value)
        {
            this.logic.Create(value);
        }

        [HttpPut]
        public void Update([FromBody] Commit value)
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
