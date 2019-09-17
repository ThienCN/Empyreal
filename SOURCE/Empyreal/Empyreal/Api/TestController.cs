using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Empyreal.Interfaces.Entities;
using Empyreal.Interfaces.Services;
using Empyreal.Models;
using Empyreal.ServiceLocators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Empyreal.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {

        // GET: api/Test
        [HttpGet("Get")]
        public List<Catalog> Get()
        {
            ICatalogService myService = ServiceLocator.Current.GetInstance<ICatalogService>();
            return myService.AllCatalog();
        }

        //[HttpGet("GetUser")]
        //public List<User> GetUser()
        //{
        //    IUserService myService = ServiceLocator.Current.GetInstance<IUserService>();
        //    return myService.AllUser();
        //}

        // GET: api/Test/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Test
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Test/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
