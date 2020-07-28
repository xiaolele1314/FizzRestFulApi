using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;

namespace WebApplication4.Controllers
{

    [Produces("application/json")]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;

        public UserController(DataContext context)
        {
            this._context = context;
        }

        // POST api/user
        [HttpPost]
        public int Post([FromBody] TbUser user)
        {
            _context.Add(user);

            _context.SaveChanges();

            return 200;
        }

        // GET api/user
        [HttpGet]
        public List<TbUser> Get()
        {
            _context.Database.EnsureCreated();
            var users = _context.tbUsers;
            List<TbUser> items = new List<TbUser>();
            foreach (var item in users)
            {
                items.Add(item);
            }
            return items;
        }

        // GET api/user/5
        [HttpGet("{id}")]
        public TbUser Get(int id)
        {
            var u = _context.tbUsers.Find(id);
            return u;
        }

        // DELETE api/user/5
        [HttpDelete("{id}")]
        public int Delete(int id)
        {
            var u = _context.tbUsers.Remove(new TbUser() { ID = id });
            _context.SaveChanges();
            return 200;
        }

        // PUT api/user/5
        [HttpPut("{id}")]
        public int Put(int id, TbUser user)
        {
            var u = _context.tbUsers.Update(user);
            _context.SaveChanges();
            return 200;
        }
    }
}
