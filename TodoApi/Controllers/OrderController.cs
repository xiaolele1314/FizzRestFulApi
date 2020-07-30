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
        //private ResultMes resultMes;
        public OrderController(OrderContext context)
        {
            this._context = context;
            //resultMes = new ResultMes();

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
                Console.WriteLine(e.Message);
                return null;
            }
        }

        // 创建用户，添加销售订单
        // POST api/{name}/order
        [HttpPost]
        public ResultMes Post(string name, [FromBody] Order order)
        {
            //建立用户
            if(findUser(name) == null)
            {
                User user = new User();
                user.name = name;
                Startup.users.Add(user);
            }
            
            //设置数据
            order.ClientName = name;
            order.CreatDate = System.DateTime.Now;
            order.UpdaeDate = order.CreatDate;
            order.CreatNo = name;
            order.UpdateNo = name;
            _context.Add(order);

             try
             {
                 _context.SaveChanges();
             }
             catch(Exception e)
             {
                 return new ResultMes { code = 202, message = e.InnerException.Message };
             }
             

            //_context.SaveChanges();

            return new ResultMes { code = 200, message = "OK"};
        }

        //为用户添加销售
        //POST api/name/order/id/details
        [HttpPost("{id:regex(^\\d{{1,10}}$)}/details")]
        public ResultMes PostDetails([FromBody] OrderDetail orderDetail,string id,string name)
        {
            //获取用户
            User user = findUser(name);

            //用户是否存在
            if(user == null)
            {
                return new ResultMes { code = 202,message = "用户不存在"};
            }

            //判断销售订单是否存在
            var f = user.no.Find(o => o == id);

            if(f == null)
            {
                return new ResultMes { code = 203, message = "该用户没有此销售订单" };
            }

            //添加销售订单明细
            orderDetail.No = id;
            orderDetail.CreatNo = name;
            orderDetail.CreatDate = System.DateTime.Now;
            orderDetail.UpdaeDate = orderDetail.CreatDate;
            orderDetail.UpdateNo = orderDetail.CreatNo;

            _context.Add(orderDetail);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return new ResultMes { code = 202, message = e.InnerException.Message };
            }

            return  new ResultMes { code = 200, message = "OK" }; ;
        }

        //查询用户所有销售订单
        // GET api/name/order
        [HttpGet]
        public object Get(string name)
        {
            //创建数据库
            _context.Database.EnsureCreated();

            //获取用户
            User user = findUser(name);
            if(user == null)
            {
                return new ResultMes { code = 202, message = "用户不存在" };
            }


            //可以更换属性更换查询方式
            //var orders = _context.orders.Where(o => o.ClientName == name).OrderBy(o => o.CreatDate);

            //根据signdate范围查询
            DateTime minDate = new DateTime(2015, 4, 23);
            DateTime maxDate = new DateTime(2020, 4, 23);
            var orders = _context.orders.Where(o => o.ClientName == name && (o.SignDate > minDate && o.SignDate <= maxDate));
            List<Order> items = new List<Order>();
            foreach (var item in orders)
            {
                items.Add(item);
                user.no.Add(item.No);
            }

           
            return pageContent(items , 2);
            //return items;
        }

        //查询用户某个销售订单
        // GET api/name/order/id
        [HttpGet("{id:regex(^\\d{{1,10}}$)}")]
        public object GetId(string id,string name)
        {
            //获取用户
            User user = findUser(name);
            if (user == null)
            {
                return new ResultMes { code = 202, message = "用户不存在" };
            }
            
            var u = _context.orders.Find(id);

            //判断订单是否存在

            if(u.ClientName != name)
            {
                return new ResultMes { code = 202, message = "订单不存在" };
            }

           
            return u;
        }

        //查询用户所有订单明细
        //GET api/name/order/details
        [HttpGet("details")]
        public object GetDetails(string name)
        {
            
            _context.Database.EnsureCreated();

            //获取用户
            User user = findUser(name);
            if (user == null)
            {
                return new ResultMes { code = 202, message = "用户不存在" };
            }


            var nos = user.no;

            List<OrderDetail> items = new List<OrderDetail>();
            foreach (string no in nos)
            {
                var details = _context.orderDetails.Where(o => o.No == no && o.CreatNo == name ).OrderBy(o => o.SortNo).ThenBy(o => o.CreatDate);
                foreach(var item in details)
                {
                    items.Add(item);
                }
            }

            //分页返回数据
            return pageContent(items,2);

            //return items;
        }

        //删除用户所有订单和订单明细
        // DELETE api/name/order
        [HttpDelete()]
        public ResultMes DeleteUser(string name)
        {
            //获取用户
            User user = findUser(name);
            if (user == null)
            {
                return new ResultMes { code = 202, message = "用户不存在" };
            }

            //删除用户的所有销售单
            var orders = _context.orders.Where(o => o.ClientName == name);
            foreach (var order in orders)
            {
                _context.orders.Remove(order);
            }

            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return new ResultMes { code = 202, message = e.InnerException.Message };
            }

            return new ResultMes { code = 200, message = "OK" };
        }

        //删除用户某个订单和订单明细
        // DELETE api/name/order/id
        [HttpDelete("{id:regex(^\\d{{1,10}}$)}")]
        public ResultMes Delete(string id,string name)
        {

            //获取用户
            User user = findUser(name);
            if (user == null)
            {
                return new ResultMes { code = 202, message = "用户不存在" };
            }

            //获取对应数据
            var f = _context.orders.Find(id);

            //只删除pending数据
            if(f.Status != (int)State.Pending)
            {
                return new ResultMes { code = 203, message = "订单状态不是pending，不能删除" };
            }
            
            //删除销售单和明细单
            var u = _context.orders.Remove(f);

            //_context.SaveChanges();

            //删除明细单
            /* var o = _context.orderDetails.Where(o => o.No == id && o.CreatNo == name);

             foreach(OrderDetail item in o)
             {
                 _context.orderDetails.Remove(item);
             }*/

            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return new ResultMes { code = 202, message = e.InnerException.Message };
            }

            return new ResultMes { code = 200, message = "OK" };
        }

        //删除用户某个销售单的明细
        // DELETE api/name/order/id/details
        [HttpDelete("{id:regex(^\\d{{1,10}}$)}/details")]
        public ResultMes DeleteDetails(string id,string name)
        {
            //获取用户
            User user = findUser(name);
            if (user == null)
            {
                return new ResultMes { code = 202, message = "用户不存在" };
            }

            var f = _context.orderDetails.Where(o => o.No == id && o.CreatNo == name);
            foreach (OrderDetail item in f)
            {
                _context.orderDetails.Remove(item);
            }

            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return new ResultMes { code = 202, message = e.InnerException.Message };
            }

            return new ResultMes { code = 200, message = "OK" };
        }

        //更新用户的销售单
        // PUT api/name/order/id
        [HttpPut("{id:regex(^\\d{{1,10}}$)}")]
        public ResultMes Put(string name,string id, [FromBody] Order order)
        {
            //获取用户
            User user = findUser(name);
            if (user == null)
            {
                return new ResultMes { code = 202, message = "用户不存在" };
            }

            if (order.No != null || order.CreatNo != null || order.CreatDate != DateTime.MinValue)
            {
                return new ResultMes { code = 203, message = "不能修改编号、创建人编号、创建日期" };
            }

            //判断数据是否存在
            if(user.no.Find(o => o == id) == null)
            {
                return new ResultMes { code = 204, message = "订单不存在" };
            }


            /*var f = _context.orders.Find(id);
            _context.SaveChanges();*/

            order.ClientName = name;
            order.UpdaeDate = System.DateTime.Now;
            order.UpdateNo = name;
            order.No = id;
            
           
            var u = _context.orders.Update(order);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return new ResultMes { code = 202, message = e.InnerException.Message };
            }

            return new ResultMes { code = 200, message = "OK" };
        }

        //更新用户的明细单
        // PUT api/name/order/id/details/d_id
        [HttpPut("{id:regex(^\\d{{1,10}}$)}/details/{d_id:regex(^\\d{{1,10}}$)}")]
        public ResultMes PutDetails(string name, string id, int d_id, [FromBody] OrderDetail detail)
        {
            //获取用户
            User user = findUser(name);
            if (user == null)
            {
                return new ResultMes { code = 202, message = "用户不存在" };
            }

            if (detail.ProNo != 0)
            {
                return new ResultMes { code = 203, message = "不能修改项目号" };
            }

            //判断订单是否存在
            if (user.no.Find(o => o == id) == null)
            {
                return new ResultMes { code = 204, message = "订单不存在" };
            }

            //判断明细是否存在
            var f = _context.orderDetails.Where(o => o.ProNo == d_id && o.No == id && o.CreatNo == name );
            if(f.Count() == 0)
            {
                return new ResultMes { code = 205, message = "订单明细不存在" };
            }
            
            detail.UpdateNo = name;
            detail.UpdaeDate = DateTime.Now;
            detail.No = id;
            detail.ProNo = d_id;

            var u = _context.orderDetails.Update(detail);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return new ResultMes { code = 202, message = e.InnerException.Message };
            }

            return new ResultMes { code = 200, message = "OK" };
        }

        //销售订单返回分页数据
         List<PageData> pageContent(List<Order> items, int num)
        {
            List<PageData> datas = new List<PageData>();

            int pageCount = items.Count() % num == 0 ? items.Count() / num : items.Count() / num + 1;
            for (int i=0;i<pageCount;i++)
            {
                PageData data = new PageData();
                data.pageNo = i + 1;
                data.pageItems = items.Skip(i * num).Take(num).ToList();

                datas.Add(data);
            }
            
            return datas;
        }

        //订单明细返回分页数据
        List<PageData> pageContent(List<OrderDetail> items, int num)
        {
            List<PageData> datas = new List<PageData>();

            int pageCount = items.Count() % num == 0 ? items.Count() / num : items.Count() / num + 1;
            for (int i = 0; i < pageCount; i++)
            {
                PageData data = new PageData();
                data.pageNo = i + 1;
                data.pageItems = items.Skip(i * num).Take(num).ToList();

                datas.Add(data);
            }

            return datas;
        }

        
    }
}
