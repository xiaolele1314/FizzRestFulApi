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
        private readonly SalesContext _context;

        public OrderService(SalesContext context)
        {
            this._context = context;
        }

        public ResultMessage<Order> CreatOrder(string userName, Order order)
        {
            if(order.No == null)
            {
                return new ResultMessage<Order> { Code = 400, Message = "没有设置用户编号", ResultObject = null };
            }
            
        
            if(_context.orders.AsNoTracking().Where(o => o.No == order.No).Count() > 0)
            {
                return new ResultMessage<Order> { Code = 400, Message = "订单编号已经存在", ResultObject = null };
            }

            //建立用户
            User user = CommonService.CreateUser(userName, order.No);

            DateTime now = System.DateTime.Now;
            order.SetCommonValue(now, userName, now, userName);

            _context.Add(order);
            _context.SaveChanges();

            return new ResultMessage<Order> { Code = 200, Message = "OK", ResultObject = order };
        }
          
        public ResultMessage<Order> DeleteOrderByKey(string userName, string orderNo)
        {
            //获取用户
            User user = CommonService.CreateUser(userName, null);

            //判断订单是否存在
            var order = _context.orders.Find(orderNo);
            if (order == null)
            {
                return new ResultMessage<Order> { Code = 204, Message = "订单不存在", ResultObject = null };
            }

            //只删除pending数据
            if (order.Status != (int)OrderStatusEnum.Pending)
            {
                return new ResultMessage<Order> { Code = 203, Message = "订单状态不是pending，不能删除" };
            }

            //删除销售单和明细单
            var u = _context.orders.Remove(order);

            _context.SaveChanges();

            return new ResultMessage<Order> { Code = 200, Message = "OK", ResultObject = null };
        }

       
        public object QueryOrderAll(string userName, MultipleGetStyleOption getStyleOption)
        {
            
            User user = CommonService.CreateUser(userName, null);

            //多种查询并分页

            //分页查询
            var results = _context.MultipleGet( getStyleOption);

            return results;
            
        }

        public object QueryOrderByKey(string orderNo)
        {
            var order = _context.orders.AsNoTracking().Where(o => o.No == orderNo).FirstOrDefault();

            //判断订单是否存在
            if (order == null)
            {
                return new ResultMessage<Order> { Code = 202, Message = "订单不存在" };
            }

            return order;
        }

        public ResultMessage<Order> UpdateOrder(string userName, string orderNo, Order order)
        {
            //获取用户
            User user = CommonService.CreateUser(userName, null);

            if (order.No != null || order.CreateUserNo != null || order.CreateUserDate != DateTime.MinValue)
            {
                return new ResultMessage<Order> { Code = 203, Message = "不能修改编号、创建人编号、创建日期", ResultObject = null };
            }

            //判断数据是否存在
            var oldOrder = _context.orders.AsNoTracking().Where(o => o.No == orderNo).FirstOrDefault();
            if (oldOrder == null)
            {
                return new ResultMessage<Order> { Code = 204, Message = "订单不存在", ResultObject = null };
            }

            //更新销售订单数据
            DateTime now = System.DateTime.Now;
            order.SetCommonValue(order.CreateUserDate, order.CreateUserNo, now, userName);
            order.No = orderNo;

            
            order.UpdateChangedField<Order>(oldOrder);

            var u = _context.orders.Update(order);
            _context.SaveChanges();

            return new ResultMessage<Order> { Code = 200, Message = "OK", ResultObject = order };
        }

 
    }
}
