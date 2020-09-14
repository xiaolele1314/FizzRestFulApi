using Fizz.SalesOrder.Extensions;
using Fizz.SalesOrder.Interface;
using Fizz.SalesOrder.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace Fizz.SalesOrder.Service
{
    public class OrderUserService : IOrderUserService
    {
        private readonly SalesContext _context;

        private readonly IUserService _userService;
        public OrderUserService(SalesContext context, IUserService userService)
        {
            this._context = context;
            this._userService = userService;
        }

        public IActionResult DeleteDetailByUser()
        {
            var details = _context.details.Where(o => o.CreateUserNo == this._userService.getUser());

            if (details.Count() == 0)
            {
                return CommonService.FailResult("订单或明细不存在");
            }

            foreach (var item in details)
            {
                _context.details.Remove(item);
            }

            _context.SaveChanges();

            return CommonService.SuccessResult("");
        }

        public IActionResult QueryDetailByUser(int? pageSize, int? pageNum)
        {
            pageSize = pageSize ?? 100;
            pageNum = pageNum ?? 1;

            var details = _context.details.AsNoTracking().Where(o => o.CreateUserNo == this._userService.getUser());

            return details.PageSales<OrderDetail>(pageSize, pageNum);
        }

        public IActionResult QureyOrderByUser(int? pageSize, int? pageNum)
        {
            pageSize = pageSize ?? 100;
            pageNum = pageNum ?? 1;
            var orders = _context.orders.AsNoTracking().Where(o => o.CreateUserNo == this._userService.getUser());

            return orders.PageSales<Order>(pageSize, pageNum);
        }

    }
}
