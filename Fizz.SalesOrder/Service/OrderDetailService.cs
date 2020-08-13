using Fizz.SalesOrder.Extensions;
using Fizz.SalesOrder.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Fizz.SalesOrder.Interface;

namespace Fizz.SalesOrder.Service
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly SalesContext _context;

        private readonly IUserService _userService;
        public OrderDetailService(SalesContext context, IUserService userService)
        {
            this._context = context;
            this._userService = userService;
        }


        public IActionResult CreateDetail(OrderDetail detail, string orderNo)
        {
            if (_context.orders.AsNoTracking().Where(o => o.No == orderNo).Count() == 0)
            {
                return new JsonResult("销售订单不存在") { StatusCode = StatusCodes.Status400BadRequest };
            }

            if(_context.details.AsNoTracking().Where(o => o.RowNo == detail.RowNo && o.OrderNo == orderNo).FirstOrDefault() != null)
            {
                return new JsonResult("该订单明细已存在") { StatusCode = StatusCodes.Status400BadRequest };
            }
            //添加销售订单明细
            detail.OrderNo = orderNo;

            DateTime now = System.DateTime.Now;

            _context.Add(detail);

            _context.SaveChanges();

            return new JsonResult(detail) { StatusCode = StatusCodes.Status200OK };
        }



        public IActionResult DeleteDetail(string orderNo, int detailNo)
        {
            var detail = _context.details.Find(new object[] { detailNo, orderNo});

            if(detail == null)
            {
                return new JsonResult("订单或明细不存在") { StatusCode = StatusCodes.Status400BadRequest };
            }
            _context.details.Remove(detail);

            _context.SaveChanges();

            return new JsonResult("") { StatusCode = StatusCodes.Status200OK };
        }

        public IActionResult DeleteDetail(string orderNo)
        {
            var details = _context.details.Where(o => o.OrderNo == orderNo);

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

        public IActionResult QueryDetail(string orderNo, int detailNo )
        {
            var order = _context.details
                .AsNoTracking()
                .Where(o => o.RowNo == detailNo && o.OrderNo == orderNo)
                .FirstOrDefault();

            return new JsonResult(order) { StatusCode = StatusCodes.Status200OK }; ;
        }

        public IActionResult QueryDetail(string orderNo, int? pageSize, int? pageNum)
        {
            var details = _context.details.AsNoTracking().Where(o => o.OrderNo == orderNo);

            PageData<OrderDetail> pageData = null;

            if(details.Count() > 0)
            {
                decimal? pageCount = Math.Ceiling((decimal)((decimal)details.Count() / pageSize));

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
     
        public IActionResult UpdateDetail(string orderNo, int detailNo, OrderDetail detail)
        {


            if (detail.RowNo != 0)
            {
                return new JsonResult("不能修改项目号") { StatusCode = StatusCodes.Status400BadRequest };
            }

            //判断明细是否存在
            var oldDetail = _context.details.Where(o => o.RowNo == detailNo && o.OrderNo == orderNo).AsNoTracking().FirstOrDefault();
            if (oldDetail == null)
            {
                return new JsonResult("订单编号或明细不存在") { StatusCode = StatusCodes.Status400BadRequest };
            }

            //修改明细数据
            DateTime now = System.DateTime.Now;
            
            detail.OrderNo = orderNo;
            detail.RowNo = detailNo;
            
            detail.UpdateChangedField<OrderDetail>(oldDetail);

            var u = _context.details.Update(detail);
            _context.SaveChanges();

            return new JsonResult(detail) { StatusCode = StatusCodes.Status200OK };
        }

       
    }
}
