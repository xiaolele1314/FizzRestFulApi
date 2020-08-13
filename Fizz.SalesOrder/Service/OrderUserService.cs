using Fizz.SalesOrder.Interface;
using Fizz.SalesOrder.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
                return new JsonResult("订单或明细不存在") { StatusCode = StatusCodes.Status400BadRequest };
            }

            foreach (var item in details)
            {
                _context.details.Remove(item);
            }

            _context.SaveChanges();

            return new JsonResult("") { StatusCode = StatusCodes.Status200OK };
        }

        public IActionResult QueryDetailByUser(int? pageSize, int? pageNum)
        {
            var details = _context.details.AsNoTracking().Where(o => o.CreateUserNo == this._userService.getUser());

            PageData<OrderDetail> pageData = null;

            if (details.Count() > 0)
            {
                decimal pageCount = Math.Ceiling((decimal)((decimal)details.Count() / pageSize));

                if (pageNum > pageCount || pageNum <= 0)
                {
                    return new JsonResult("pageNum错误") { StatusCode = StatusCodes.Status400BadRequest };
                }
                //分页查询

                details = details
                    .Skip((int)(pageSize * (pageNum - 1)))
                    .Take((int)pageSize);

                pageData = new PageData<OrderDetail> { PageCount = (int)pageCount, PageNum = pageNum, PageItems = details.ToList() };
            }
            

            return new JsonResult(pageData) { StatusCode = StatusCodes.Status200OK };
        }

        public IActionResult QureyOrderByUser(int? pageSize, int? pageNum)
        {
            var orders = _context.orders.AsNoTracking().Where(o => o.CreateUserNo == this._userService.getUser());

            PageData<Order> pageData = null;
            if (orders.Count() > 0)
            {
                int pageCount = (int)Math.Ceiling((decimal)((decimal)orders.Count() / pageSize));

                if (pageNum > pageCount || pageNum <= 0)
                {
                    return new JsonResult("pageNum错误") { StatusCode = StatusCodes.Status400BadRequest };
                }

                //分页查询            
                orders = orders.Skip((int)(pageSize * (pageNum - 1))).Take((int)pageSize);

                pageData = new PageData<Order> { PageCount = (int)pageCount, PageNum = pageNum, PageItems = orders.ToList() };
            }
            

            return new JsonResult(pageData) { StatusCode = StatusCodes.Status200OK };
        }

    }
}
