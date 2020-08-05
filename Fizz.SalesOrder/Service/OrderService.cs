using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Fizz.SalesOrder.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Fizz.SalesOrder.Service
{
    public class OrderService : IOrderService
    {
        private readonly OrderContext _context;

        public OrderService(OrderContext context)
        {
            this._context = context;

            //查询数据库为user添加no
            foreach(User user in Startup.users)
            {
                var orders = _context.orders.Where(o => o.ClientName == user.Name).AsNoTracking();
                foreach(Order order in orders)
                {
                    if(!user.No.Contains(order.No))
                        user.No.Add(order.No);
                }
            }
        }

        public ResultMes CreatOrder(string name, Order order)
        {

            //建立用户
            User user = creatUser(name,order.No);

            //设置数据
            order.ClientName = name;

            DateTime now = System.DateTime.Now;
            order = configCreatUpdateNoDate<Order>(order, now, name,now, name);
    
            _context.Add(order);

            _context.SaveChanges();

            return new ResultMes { Code = 200, Message = "OK" };
        }

        public ResultMes CreatOrderDetails(OrderDetail orderDetail, string orderNo, string name)
        {
            //获取用户
            User user = creatUser(name,null);
            
            //判断销售订单是否存在
            var f = user.No.Find(o => o == orderNo);

            if (f == null)
            {
                return new ResultMes { Code = 203, Message = "该用户没有此销售订单" };
            }

            //添加销售订单明细
            orderDetail.No = orderNo;

            DateTime now = System.DateTime.Now;
            orderDetail = configCreatUpdateNoDate<OrderDetail>(orderDetail, now, name, now, name);
            //ConfigCreatUpdateNoDate<OrderDetail>(orderDetail, System.DateTime.Now, name, orderDetail.CreatDate, orderDetail.CreatNo);
            //orderDetail.CreatNo = name;
            //orderDetail.CreatDate = System.DateTime.Now;
            //orderDetail.UpdaeDate = orderDetail.CreatDate;
            //orderDetail.UpdateNo = orderDetail.CreatNo;

            _context.Add(orderDetail);

             _context.SaveChanges();

            return new ResultMes { Code = 200, Message = "OK" };
        }

        public ResultMes DeleteOrderDetailById(string name, string orderNo)
        {
            //获取用户
            User user = creatUser(name, null);

            //判断订单是否存在
            if (user.No.Find(o => o == orderNo) == null)
            {
                return new ResultMes { Code = 204, Message = "订单不存在" };
            }

            var f = _context.orderDetails.Where(o => o.No == orderNo && o.CreatNo == name);
            foreach (OrderDetail item in f)
            {
                _context.orderDetails.Remove(item);
            }

            _context.SaveChanges();

            return new ResultMes { Code = 200, Message = "OK" };
        }

        public ResultMes DeleteUserAll(string name)
        {
            //获取用户
            User user = findUser(name);
            if (user == null)
            {
                user = new User { Name = name, No = new List<string>() };
                Startup.users.Add(user);

                return new ResultMes { Code = 202, Message = "此用户无订单" };
            }

            //删除用户的所有销售单
            var orders = _context.orders.Where(o => o.ClientName == name);
            foreach (var order in orders)
            {
                _context.orders.Remove(order);
            }

             _context.SaveChanges();

            return new ResultMes { Code = 200, Message = "OK" };
        }

        public ResultMes DeleteUserOrder(string name, string orderNo)
        {
            //获取用户
            User user = creatUser(name, null);

            //判断订单是否存在
            if (user.No.Find(o => o == orderNo) == null)
            {
                return new ResultMes { Code = 204, Message = "订单不存在" };
            }

            //获取对应数据
            var f = _context.orders.Where(o => o.No == orderNo && o.ClientName == user.Name).First();

            //只删除pending数据
            if (f.Status != (int)State.Pending)
            {
                return new ResultMes { Code = 203, Message = "订单状态不是pending，不能删除" };
            }

            //删除销售单和明细单
            var u = _context.orders.Remove(f);

            _context.SaveChanges();


            return new ResultMes { Code = 200, Message = "OK" };
        }

        public List<PageData<Order>> QueryUserAllOrder(string name, int pageSize, int pageNum)
        {
            //创建数据库
            //_context.Database.EnsureCreated();

            //获取用户
            User user = creatUser(name,null);

            //可以更换属性更换查询方式
            //var orders = _context.orders.Where(o => o.ClientName == name).OrderBy(o => o.CreatDate);
            
            //分页查询
            var orderPages = pageContent<Order>(pageSize, pageNum, user);

            //根据signdate范围查询
            //DateTime minDate = new DateTime(2015, 4, 23);
            //DateTime maxDate = new DateTime(2020, 4, 23);
            //var orders = _context.orders.Where(o => o.ClientName == name && (o.SignDate > minDate && o.SignDate <= maxDate));

            //List<PageData<Order>> items = new List<PageData<Order>>();
            //foreach (var item in orderPages)
            //{
            //    items.Add(item);
            //}

            return orderPages;
            //return items;
        }

        public List<PageData<OrderDetail>> QueryUserAllOrderDetail(string name, int pageSize, int pageNum)
        {
           // _context.Database.EnsureCreated();

            //获取用户
            User user = creatUser(name,null);

            //var nos = user.No;

            //List<OrderDetail> items = new List<OrderDetail>();
            //foreach (string no in nos)
            //{
            //    var details = _context.orderDetails.Where(o => o.No == no && o.CreatNo == name).OrderBy(o => o.SortNo).ThenBy(o => o.CreatDate);
            //    foreach (var item in details)
            //    {
            //        items.Add(item);
            //    }
            //}
            var detailPages = pageContent<OrderDetail>(pageSize, pageNum, user);
            //分页返回数据
            //return pageContent(items, 2);

            return detailPages;
        }

        public object QueryUserOrder(string name, string orderNo, int pageSize, int pageNum)
        {
            //获取用户
            User user = creatUser(name,null);
           
            var u = _context.orders.Find(orderNo);

            //判断订单是否存在

            if (u.ClientName != name)
            {
                return new ResultMes { Code = 202, Message = "订单不存在" };
            }


            return u;
        }

        public ResultMes UpdateOrder(string name, string orderNo, Order order)
        {
            //获取用户
            User user = creatUser(name, null);

            if (order.No != null || order.CreatNo != null || order.CreatDate != DateTime.MinValue)
            {
                return new ResultMes { Code = 203, Message = "不能修改编号、创建人编号、创建日期" };
            }

            //判断数据是否存在
            if (user.No.Find(o => o == orderNo) == null)
            {
                return new ResultMes { Code = 204, Message = "订单不存在" };
            }


            //更新销售订单数据
            DateTime now = System.DateTime.Now;
            order = configCreatUpdateNoDate<Order>(order, order.CreatDate, order.CreatNo, now, name);
            order.No = orderNo;
            order.ClientName = name;

            var oldOrder = _context.orders.AsNoTracking().Where(o => o.No == orderNo).FirstOrDefault();
            order = updateOrderOrDetail(order, oldOrder, new Order());

            //order.UpdaeDate = System.DateTime.Now;
            //order.UpdateNo = name;
            //var currentEntry = _context.ChangeTracker.Entries().FirstOrDefault();
            //currentEntry.State = EntityState.Detached;

            //var _order = _context.orders.Find(orderNo);

            //_context.orders.Update(order);
            //List<EntityEntry> le = _context.ChangeTracker.Entries().ToList();
            //foreach(EntityEntry e in le)
            //{
            //    EntityState s = e.State;
            //}
            var u = _context.orders.Update(order);

            _context.SaveChanges();

            return new ResultMes { Code = 200, Message = "OK" };
        }

        public ResultMes UpdateOrderDetail(string name, string orderNo, int detailNo, OrderDetail detail)
        {
            //获取用户
            User user = creatUser(name,null);
            

            if (detail.ProNo != 0)
            {
                return new ResultMes { Code = 203, Message = "不能修改项目号" };
            }

            //判断订单是否存在
            if (user.No.Find(o => o == orderNo) == null)
            {
                return new ResultMes { Code = 204, Message = "订单不存在" };
            }

            //判断明细是否存在
            var oldDetail = _context.orderDetails.Where(o => o.ProNo == detailNo).AsNoTracking().FirstOrDefault();
            if (oldDetail == null)
            {
                return new ResultMes { Code = 205, Message = "订单明细不存在" };
            }

            //修改明细数据
            DateTime now = System.DateTime.Now;
            detail = configCreatUpdateNoDate<OrderDetail>(detail, detail.CreatDate, detail.CreatNo, now, name);
            //detail.UpdateNo = name;
            //detail.UpdaeDate = DateTime.Now;
            detail.No = orderNo;
            detail.ProNo = detailNo;

            //EntityState state = _context.Entry(detail).State;

            //var oldDetail = _context.orderDetails.AsNoTracking().Where(o => o.No == orderNo && o.ProNo == detailNo).FirstOrDefault();
            detail = updateOrderOrDetail<OrderDetail>(detail, oldDetail, new OrderDetail());

            var u = _context.orderDetails.Update(detail);
           

            _context.SaveChanges();

            return new ResultMes { Code = 200, Message = "OK" };
        }

        //根据name查找用户
        User findUser(string name)
        {
            try
            {
                User user = Startup.users.Where(o => o.Name == name).First();
                return user;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        //创建用户
        User creatUser(string name, string no)
        {
            User user = findUser(name);
            if(user == null)
            {
                user = new User { Name = name };
                user.No = new List<string>();
                userAddNo(user, no);
                Startup.users.Add(user);
            }
            else
            {
                userAddNo(user, no);
            }

            return user;
        }

        //用户添加no
        void userAddNo(User user, string no)
        {
            if (null != no)
            {
                if (!user.No.Contains(no))
                    user.No.Add(no);
            }
        }

      
        //销售订单返回分页数据
        List<PageData<T>> pageContent<T>(int pageSize, int pageNum, User user)
        {
            List<PageData<T>> pageDatas = new List<PageData<T>>();
            

            if(typeof(T).Name.Equals("Order"))
            {
                decimal count = Math.Ceiling((decimal)_context.orders.Count() / pageSize);
                var orders =  _context.orders.Skip(pageSize * (pageNum - 1)).Take(pageSize).ToList();
                foreach (var order in orders)
                {
                    PageData<T> pageData = new PageData<T>
                    {
                        PageCount = (int)count,
                        PageNo = pageNum,
                        PageItems = (T)(object)order
                    };

                    pageDatas.Add(pageData);
                }
            }
            else if(typeof(T).Name.Equals("OrderDetail"))
            {
                decimal count = Math.Ceiling((decimal)_context.orderDetails.Count() / pageSize);
                //var details = _context.orderDetails.AsNoTracking().Where(o => user.No.Contains(o.No)).Skip(pageSize * (pageNum - 1)).Take(pageSize).ToList();
                var details = _context.orderDetails.Where(o => user.No.Contains(o.No)).Skip(pageSize * (pageNum - 1)).Take(pageSize).ToList();
                foreach (var detail in details)
                {
                    PageData<T> pageData = new PageData<T>
                    {
                        PageCount = (int)count,
                        PageNo = pageNum,
                        PageItems = (T)(object)detail
                    };

                    pageDatas.Add(pageData);
                }
            }
            


            return pageDatas;
        }

        //配置创建者和更新者日期和编号
         T configCreatUpdateNoDate<T>(T content, DateTime creatDate, string creatNo, DateTime updateDate, string updateNo) 
        {
            if(content is Order)
            {
                Order order = content as Order;
                order.CreatDate = creatDate;
                order.CreatNo = creatNo;
                order.UpdateNo = updateNo;
                order.UpdaeDate = updateDate;
                return (T)(object)order;
            }
            else
            {
                OrderDetail detail = content as OrderDetail;
                detail.CreatDate = creatDate;
                detail.CreatNo = creatNo;
                detail.UpdateNo = updateNo;
                detail.UpdaeDate = updateDate;
                return (T)(object)detail;
            }
        }


        //获取要更新列，通过赋值只更新改变的字段
        T updateOrderOrDetail<T>(T now, T old, T origin)
        {
            Type t_new = now.GetType();
            Type t = origin.GetType();
            Type t_old = old.GetType();

            //把order属性及其值用dictionary存储起来
            //Dictionary<string, object> dic = new Dictionary<string, object>();
            PropertyInfo[] pi_new = t_new.GetProperties();
            PropertyInfo[] pi = t.GetProperties();
            PropertyInfo[] pi_old = t_old.GetProperties();

            int len = pi.Length;
            for(int i=0; i<len; i++)
            {
                var ob_new = pi_new[i].GetValue(now);
                var ob = pi[i].GetValue(origin);
                var ob_old = pi_old[i].GetValue(old);
                if(ob_new == null || ob_new.Equals(ob))
                {
                    pi_new[i].SetValue(now, ob_old);
                }
            }

            return now;
        }
    }
}
