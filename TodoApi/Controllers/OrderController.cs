using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Produces("application/json")]
    [Route("api/user")]
    public class OrderController
    {
        private readonly OrderContext _context;

        public OrderController(OrderContext context)
        {
            this._context = context;
        }

        // POST api/user
        [HttpPost]
        public int Post([FromBody] Order order)
        {
            _context.Add(order);

            _context.SaveChanges();

            return 200;
        }

        // GET api/user
        [HttpGet]
        public List<Order> Get()
        {
            _context.Database.EnsureCreated();
            var orders = _context.orders;
            List<Order> items = new List<Order>();
            foreach (var item in orders)
            {
                items.Add(item);
            }
            return items;
        }

        // GET api/user/5
        [HttpGet("{id}")]
        public Order Get(int id)
        {
            var u = _context.orders.Find(id);
            return u;
        }

        // DELETE api/user/5
        [HttpDelete("{id}")]
        public int Delete(string id)
        {
            var u = _context.orders.Remove(new Order() { No = id });
            _context.SaveChanges();
            return 200;
        }

        // PUT api/user/5
        [HttpPut("{id}")]
        public int Put(int id, Order order)
        {
            var u = _context.orders.Update(order);
            _context.SaveChanges();
            return 200;
        }
    }
}
