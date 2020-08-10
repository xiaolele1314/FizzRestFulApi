using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Fizz.SalesOrder.Extensions;
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
        }

        public ResultMes CreatOrder(string userName, Order order)
        {
            if(order.No == null)
            {
                return new ResultMes { Code = 400, Message = "没有设置用户编号" };
            }
            
        
            if(_context.orders.AsNoTracking().Where(o => o.No == order.No).Count() > 0)
            {
                return new ResultMes { Code = 400, Message = "该用户编号已经存在" };
            }

            //建立用户
            User user = CommonService.CreateUser(userName, order.No);

            DateTime now = System.DateTime.Now;
            order.SetCommonValue(now, userName, now, userName);
    
            _context.Add(order);

            _context.SaveChanges();

            return new ResultMes { Code = 200, Message = "OK" };
        }
          
      
        public ResultMes DeleteOrderByKey(string userName, string orderNo)
        {
            //获取用户
            User user = CommonService.CreateUser(userName, null);

            //判断订单是否存在
            if (user.OrderNos.Find(o => o == orderNo) == null)
            {
                return new ResultMes { Code = 204, Message = "订单不存在" };
            }

            //获取对应数据
            var f = _context.orders.Where(o => o.No == orderNo && o.CreateUserNo == user.Name).First();

            //只删除pending数据
            if (f.Status != (int)OrderStatusEnum.Pending)
            {
                return new ResultMes { Code = 203, Message = "订单状态不是pending，不能删除" };
            }

            //删除销售单和明细单
            var u = _context.orders.Remove(f);

            _context.SaveChanges();

            return new ResultMes { Code = 200, Message = "OK" };
        }

       
        public PageData<Order> QueryOrderAll(string userName, string propertyName, MultipleGetStyleOption getStyleOption, int pageSize, int pageNum)
        {
            
            User user = CommonService.CreateUser(userName, null);

            if (pageNum < 0)
            {
                throw new Exception("pageNum error!");
            }

            decimal pageCount = Math.Ceiling((decimal)_context.orders.Count() / pageSize);

            if (pageNum > pageCount)
            {
                throw new Exception("pageNum too large!");
            }

            //多种查询并分页

            //分页查询
            var orderPages = _context.MultipleGet(propertyName, getStyleOption, pageSize, pageNum);

            return new PageData<Order> { PageNo = pageNum, PageCount = pageCount, PageItems = orderPages };
            
        }

        public object QueryOrderByKey(string userName, string orderNo)
        {
            //获取用户
            User user = CommonService.CreateUser(userName, null);

            var order = _context.orders.AsNoTracking().Where(o => o.No == orderNo).FirstOrDefault();

            //判断订单是否存在
            if (order.ClientName != userName)
            {
                return new ResultMes { Code = 202, Message = "订单不存在" };
            }

            return order;
        }

        public PageData<Order> QureyOrderByUser(string userName, int pageSize, int pageNum)
        {

            //获取用户
            User user = CommonService.CreateUser(userName, null);           

            if (pageNum < 0)
            {
                throw new Exception("pageNum error!");
            }
           
            decimal pageCount = Math.Ceiling((decimal)_context.orders.Count() / pageSize);

            if (pageNum > pageCount)
            {
                throw new Exception("pageNum too large!");
            }

            //分页查询            
            var orderPages = _context.orders.AsNoTracking().Where(o => o.CreateUserNo == userName).Skip(pageSize * (pageNum - 1)).Take(pageSize).ToList();

            return new PageData<Order> { PageNo = pageNum, PageCount = pageCount, PageItems = orderPages };
        }

        public ResultMes UpdateOrder(string userName, string orderNo, Order order)
        {
            //获取用户
            User user = CommonService.CreateUser(userName, null);

            if (order.No != null || order.CreateUserNo != null || order.CreateUserDate != DateTime.MinValue)
            {
                return new ResultMes { Code = 203, Message = "不能修改编号、创建人编号、创建日期" };
            }

            //判断数据是否存在
            if (user.OrderNos.Find(o => o == orderNo) == null)
            {
                return new ResultMes { Code = 204, Message = "订单不存在" };
            }

            //更新销售订单数据
            DateTime now = System.DateTime.Now;
            order.SetCommonValue(order.CreateUserDate, order.CreateUserNo, now, userName);
            order.No = orderNo;

            var oldOrder = _context.orders.AsNoTracking().Where(o => o.No == orderNo).FirstOrDefault();
            order.UpdateChangedField<Order>(oldOrder);

            var u = _context.orders.Update(order);

            _context.SaveChanges();

            return new ResultMes { Code = 200, Message = "OK" };
        }

 
    }
}
