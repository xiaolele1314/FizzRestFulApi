using Fizz.SalesOrder.Interface;
using Fizz.SalesOrder.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fizz.SalesOrder.Service
{
    public class OrderUserService:IOrderUserService
    {
        private readonly SalesContext _context;

        public OrderUserService(SalesContext context)
        {
            this._context = context;
        }

        public ResultMessage<OrderDetail> DeleteDetailByUser(string userName)
        {
            //获取用户
            User user = CommonService.CreateUser(userName, null);

            var details = _context.details.Where(o => user.OrderNos.Contains(o.OrderNo));

            if (details.Count() == 0)
            {
                return new ResultMessage<OrderDetail> { Code = 400, Message = "订单或明细不存在", ResultObject = null };
            }

            foreach (var item in details)
            {
                _context.details.Remove(item);
            }

            _context.SaveChanges();

            return new ResultMessage<OrderDetail> { Code = 200, Message = "OK", ResultObject = null };
        }

        public object QueryDetailByUser(string userName, int? pageSize, int? pageNum)
        {

            //获取用户
            User user = CommonService.CreateUser(userName, null);

            var details = _context.details.AsNoTracking().Where(o => user.OrderNos.Contains(o.OrderNo));
            if (details.Count() == 0)
            {
                return new ResultMessage<OrderDetail> { Code = 400, Message = "该用户下没有订单或明细", ResultObject = null };
            }
            decimal pageCount = Math.Ceiling((decimal)((decimal)details.Count() / pageSize));

            if (pageNum > pageCount || pageNum <= 0)
            {
                return new ResultMessage<OrderDetail> { Code = 400, Message = "pageNum错误", ResultObject = null };
            }
            //分页查询

            var orderPages = details
                .Skip((int)(pageSize * (pageNum - 1)))
                .Take((int)pageSize)
                .ToList();

            if (orderPages == null)
            {
                return new ResultMessage<OrderDetail> { Code = 400, Message = "订单编号或明细不存在", ResultObject = null };
            }

            return new PageData<OrderDetail> { PageNum = pageNum, PageCount = (int)pageCount, PageItems = orderPages };
        }

        public object QureyOrderByUser(string userName, int? pageSize, int? pageNum)
        {

            //获取用户
            User user = CommonService.CreateUser(userName, null);

            var ordersQuery = _context.orders.AsNoTracking().Where(o => o.CreateUserNo == userName);
            int pageCount = (int)Math.Ceiling((decimal)((decimal)ordersQuery.Count() / pageSize));

            if (pageNum > pageCount || pageNum <= 0)
            {
                return new ResultMessage<OrderDetail> { Code = 400, Message = "pageNum错误", ResultObject = null };
            }

            //分页查询            
            var orderPages = ordersQuery.Skip((int)(pageSize * (pageNum - 1))).Take((int)pageSize).ToList();

            return new PageData<Order> { PageNum = pageNum, PageCount = pageCount, PageItems = orderPages };
        }

    }
}
