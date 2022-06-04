using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using api.Interfaces;
using api.Model;
using api.Data;
using Microsoft.AspNetCore.Cors;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        // GET: api/Person
        [EnableCors("OpenPolicy")]
        [HttpGet]
        public List<Person> Get()
        {
            IPersonDataHandler dataHandler = new PersonDataHandler();
            return dataHandler.Select();
        }

        // GET: api/Person/5
        [EnableCors("OpenPolicy")]
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Person
        [EnableCors("OpenPolicy")]
        [HttpPost]
        public void Post([FromBody] Person value)
        {
            //IPersonDataHandler dataHandler = new PersonDataHandler(); used this to test with postman
            //dataHandler.Insert(value);
            value.DataHandler.Insert(value);
        }

        // PUT: api/Person/5
        [EnableCors("OpenPolicy")]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Person value)
        {
            value.DataHandler.Update(value);
        }

        // DELETE: api/Person/5
        [EnableCors("OpenPolicy")]
        [HttpDelete("{id}")]
        public void Delete(int id, [FromBody] Person value)
        {
            value.DataHandler.Delete(value);
        }
    }
}
