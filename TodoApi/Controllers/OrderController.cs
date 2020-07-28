using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Produces("application/json")]
    [Route("api/order")]
    public class OrderController : ControllerBase
    {
        private readonly OrderContext _context;


        public OrderController(OrderContext context)
        {
            this._context = context;
        }

        // POST api/order
        [HttpPost]
        public int Post([FromBody] Order order)
        {
            //设置数据
            order.CreatDate = System.DateTime.Now;
            order.UpdaeDate = order.CreatDate;
            order.CreatNo = "001";
            order.UpdateNo = "001";
            _context.Add(order);

            _context.SaveChanges();

            return 200;
        }

        //POST api/order/id/details
        [HttpPost("{id}/details")]
        public int PostDetails([FromBody] OrderDetail orderDetail,string id)
        {
            var f = _context.orders.Find(id);

            orderDetail.No = id;
            orderDetail.CreatNo = "003";
            orderDetail.CreatDate = System.DateTime.Now;
            orderDetail.UpdaeDate = orderDetail.CreatDate;
            orderDetail.UpdateNo = orderDetail.CreatNo;

            _context.Add(orderDetail);
            _context.SaveChanges();

            return 200;
        }

        // GET api/order
        [HttpGet]
        public List<Order> Get()
        {
            //创建数据库
            _context.Database.EnsureCreated();
            var orders = _context.orders;
            List<Order> items = new List<Order>();
            foreach (var item in orders)
            {
                items.Add(item);
            }
            return items;
        }

        // GET api/order/id
        [HttpGet("{id}")]
        public Order Get(string id)
        {
            var u = _context.orders.Find(id);
            return u;
        }

        //GET api/order/id/details
        [HttpGet("{id}/details")]
        public List<OrderDetail> GetDetails(string id)
        {
            _context.Database.EnsureCreated();

            var no = _context.orders.Find(id).No;
            var details = _context.orderDetails.Where(o => o.No == no);

            List<OrderDetail> items = new List<OrderDetail>();
            foreach(var item in details)
            {
                items.Add(item);
            }
            return items;
        }

        // DELETE api/order/id
        [HttpDelete("{id}")]
        public int Delete(string id)
        {
            //获取对应数据
            var f = _context.orders.Find(id);

            //只删除pending数据
            if(f.Status != (int)State.Pending)
            {
                return 203;
            }
            
            //删除销售单
            var u = _context.orders.Remove(f);

            //_context.SaveChanges();

            //删除明细单
            var o = _context.orderDetails.Where(o => o.No == id);

            foreach(OrderDetail item in o)
            {
                _context.orderDetails.Remove(item);
            }

            _context.SaveChanges();
            return 200;
        }

        // DELETE api/order/id/details
        [HttpDelete("{id}/details")]
        public int DeleteDetails(string id)
        {
            var f = _context.orderDetails.Where(o => o.No == id);
            foreach (OrderDetail item in f)
            {
                _context.orderDetails.Remove(item);
            }
            _context.SaveChanges();

            return 200;
        }


        // PUT api/order/id
        [HttpPut("{id}")]
        public int Put(string id, [FromBody] Order order)
        {
            if (order.No != null || order.CreatNo != null || order.CreatDate != DateTime.MinValue)
            {
                return 202;
            }

            /*var f = _context.orders.Find(id);
            _context.SaveChanges();*/
            
            order.UpdaeDate = System.DateTime.Now;
            order.UpdateNo = "002";
            order.No = id;
            
            var u = _context.orders.Update(order);
            _context.SaveChanges();
            return 200;
        }

        // PUT api/order/id/details/d_id
        [HttpPut("{id}/details/{d_id}")]
        public int PutDetails(string id, int d_id, [FromBody] OrderDetail detail)
        {
            if(detail.ProNo != 0)
            {
                return 202;
            }

            detail.UpdateNo = "004";
            detail.UpdaeDate = DateTime.Now;
            detail.No = id;
            detail.ProNo = d_id;

            var u = _context.orderDetails.Update(detail);
            _context.SaveChanges();
            return 200;
        }
       
    }
}
