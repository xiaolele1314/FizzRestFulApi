using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Produces("application/json")]
    [Route("api/{name:regex(^\\D{{1,5}})}/order")]
    public class OrderController : ControllerBase
    {
        private readonly OrderContext _context;
        
        public OrderController(OrderContext context)
        {
            this._context = context;

           

        }

        //根据name查找用户
        User findUser(string name)
        {
            try
            {
                User user = Startup.users.Where(o => o.name == name).First();
                return user;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        // POST api/{name}/order
        [HttpPost]
        public int Post(string name, [FromBody] Order order)
        {
            //建立用户
            if(findUser(name) == null)
            {
                User user = new User();
                user.name = name;
                Startup.users.Add(user);
                _context.SaveChanges();
            }
            
            //设置数据
            order.ClientName = name;
            order.CreatDate = System.DateTime.Now;
            order.UpdaeDate = order.CreatDate;
            order.CreatNo = name;
            order.UpdateNo = name;
            _context.Add(order);
        
            _context.SaveChanges();

            return 200;
        }

        //POST api/name/order/id/details
        [HttpPost("{id:regex(^\\d{{1,10}}$)}/details")]
        public int PostDetails([FromBody] OrderDetail orderDetail,string id,string name)
        {
            //获取用户
            User user = findUser(name);

            //用户是否存在
            if(user == null)
            {
                return 202;
            }

            //判断销售订单是否存在
            var f = _context.orders.Find(id);

            if(f == null)
            {
                return 203;
            }

            //添加销售订单明细
            orderDetail.No = id;
            orderDetail.CreatNo = name;
            orderDetail.CreatDate = System.DateTime.Now;
            orderDetail.UpdaeDate = orderDetail.CreatDate;
            orderDetail.UpdateNo = orderDetail.CreatNo;

            _context.Add(orderDetail);
            _context.SaveChanges();

            return 200;
        }

        // GET api/name/order
        [HttpGet]
        public List<Order> Get(string name)
        {
            //创建数据库
            _context.Database.EnsureCreated();

            //获取用户
            User user = findUser(name);
            if(user == null)
            {
                return null;
            }

 
            var orders = _context.orders.Where(o => o.ClientName == name);
            List<Order> items = new List<Order>();
            foreach (var item in orders)
            {
                items.Add(item);

                //test
                user.no.Add(item.No);
                //Startup.s_user.no.Add(item.No);
            }
            return items;
        }

        // GET api/name/order/id
        [HttpGet("{id:regex(^\\d{{1,10}}$)}")]
        public Order GetId(string id,string name)
        {
            //获取用户
            User user = findUser(name);
            if (user == null)
            {
                return null;
            }

            //判断查询用户是否一样
            if(user.name != name)
            {
                return null;
            }

            //判断订单是否存在
            try
            {
                user.no.Where(o => o == id);
            }
            catch(Exception e)
            {
                return null;
            }
            

            var u = _context.orders.Find(id);
            return u;
        }

        //GET api/name/order/details
        [HttpGet("details")]
        public List<OrderDetail> GetDetails(string name)
        {
            
            _context.Database.EnsureCreated();

            //获取用户
            User user = findUser(name);
            if (user == null)
            {
                return null;
            }


            var nos = user.no;

            List<OrderDetail> items = new List<OrderDetail>();
            foreach (string no in nos)
            {
                var details = _context.orderDetails.Where(o => o.No == no);
                foreach(var item in details)
                {
                    items.Add(item);
                }
            }
            
            return items;
        }

        // DELETE api/name/order
        [HttpDelete()]
        public int DeleteUser(string name)
        {
            //获取用户
            User user = findUser(name);
            if (user == null)
            {
                return 202;
            }

            //删除用户的所有销售单
            var orders = _context.orders.Where(o => o.ClientName == name);
            foreach (var order in orders)
            {
                var details = _context.orderDetails.Where(o => o.No == order.No);
                _context.SaveChanges();
                foreach (var detail in details)
                {
                    _context.orderDetails.Remove(detail);

                }
                _context.orders.Remove(order);
            }

            _context.SaveChanges();

            return 200;
        }
        // DELETE api/name/order/id/single
        [HttpDelete("{id:regex(^\\d{{1,10}}$)}")]
        public int Delete(string id,string name)
        {

            //获取用户
            User user = findUser(name);
            if (user == null)
            {
                return 202;
            }

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

        // DELETE api/name/order/id/details
        [HttpDelete("{id:regex(^\\d{{1,10}}$)}/details")]
        public int DeleteDetails(string id,string name)
        {
            //获取用户
            User user = findUser(name);
            if (user == null)
            {
                return 202;
            }

            var f = _context.orderDetails.Where(o => o.No == id);
            foreach (OrderDetail item in f)
            {
                _context.orderDetails.Remove(item);
            }
            _context.SaveChanges();

            return 200;
        }


        // PUT api/name/order/id
        [HttpPut("{id:regex(^\\d{{1,10}}$)}")]
        public int Put(string name,string id, [FromBody] Order order)
        {
            //获取用户
            User user = findUser(name);
            if (user == null)
            {
                return 202;
            }

            if (order.No != null || order.CreatNo != null || order.CreatDate != DateTime.MinValue)
            {
                return 203;
            }

            //判断数据是否存在
            if(user.no.Find(o => o == id) == null)
            {
                return 203;
            }


            /*var f = _context.orders.Find(id);
            _context.SaveChanges();*/

            order.ClientName = name;
            order.UpdaeDate = System.DateTime.Now;
            order.UpdateNo = name;
            order.No = id;
            
           
            var u = _context.orders.Update(order);
            _context.SaveChanges();

            return 200;
        }

        // PUT api/name/order/id/details/d_id
        [HttpPut("{id:regex(^\\d{{1,10}}$)}/details/{d_id:regex(^\\d{{1,10}}$)}")]
        public int PutDetails(string name, string id, int d_id, [FromBody] OrderDetail detail)
        {
            //获取用户
            User user = findUser(name);
            if (user == null)
            {
                return 202;
            }

            if (detail.ProNo != 0)
            {
                return 203;
            }

            //判断订单是否存在
            if (user.no.Find(o => o == id) == null)
            {
                return 203;
            }

            //判断明细是否存在
            try
            {
                _context.orderDetails.Where(o => o.ProNo == d_id);
            }
            catch(Exception e)
            {
                return 206;
            }

            detail.UpdateNo = name;
            detail.UpdaeDate = DateTime.Now;
            detail.No = id;
            detail.ProNo = d_id;

            var u = _context.orderDetails.Update(detail);
            _context.SaveChanges();
            return 200;
        }
       
    }
}
