using Fizz.SalesOrder.Extensions;
using Fizz.SalesOrder.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace Fizz.SalesOrder.Service
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly SalesContext _context;

        private readonly IMapper _mapper;

        public OrderDetailService(SalesContext context,  IMapper mapper)
            :base()
        {
            this._mapper = mapper;
            this._context = context;
        }

        public IActionResult CreateDetail(OrderDetail detail, string orderNo)
        {
            //添加销售订单明细
            detail.OrderNo = orderNo;

            if (_context.orders.AsNoTracking().Where(o => o.No == orderNo).Count() == 0)
            {
                return CommonService.FailResult("销售订单不存在");
            }

            if(_context.details.AsNoTracking().Where(o => o.RowNo == detail.RowNo && o.OrderNo == orderNo).FirstOrDefault() != null)
            {
                return CommonService.FailResult("该订单明细已存在");
            }
            

            DateTime now = System.DateTime.Now;

            _context.Add(detail);

            _context.SaveChanges();

            return CommonService.SuccessResult(detail);
        }



        public IActionResult DeleteDetail(string orderNo, int detailNo)
        {
            var detail = _context.details.Find(new object[] { detailNo, orderNo});

            if(detail == null)
            {
                return CommonService.FailResult("订单或明细不存在");
            }
            _context.details.Remove(detail);

            _context.SaveChanges();

            return CommonService.SuccessResult("");
        }

        public IActionResult DeleteDetail(string orderNo)
        {
            var details = _context.details.Where(o => o.OrderNo == orderNo);

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

        public IActionResult QueryDetail(string orderNo, int detailNo )
        {
            var order = _context.details
                .AsNoTracking()
                .Where(o => o.RowNo == detailNo && o.OrderNo == orderNo)
                .FirstOrDefault();
            if(order == null)
            {
                return CommonService.FailResult("没有此明细单");
            }
            return CommonService.SuccessResult(order);
        }

        public IActionResult QueryDetail(string orderNo, int? pageSize, int? pageNum)
        {
            pageNum = pageNum ?? 1;
            pageSize = pageSize ?? 100;
            var details = _context.details.AsNoTracking().Where(o => o.OrderNo == orderNo);

            return details.PageSales<OrderDetail>(pageSize, pageNum);
        }
     
        public IActionResult UpdateDetail(string orderNo, int detailNo, OrderDetailDto detailDto)
        {


            if (detailDto.RowNo != 0)
            {
                return CommonService.FailResult("不能修改项目号");
            }

            //判断明细是否存在
            var detail = _context.details.Where(o => o.RowNo == detailNo && o.OrderNo == orderNo).AsNoTracking().FirstOrDefault();
            if (detail == null)
            {
                return CommonService.FailResult("订单编号或明细不存在");
            }

            //修改明细数据
            DateTime now = System.DateTime.Now;
            
            detailDto.OrderNo = orderNo;
            detailDto.RowNo = detailNo;

            //detail.UpdateChangedField<OrderDetail>(oldDetail);

            this._mapper.Map(detailDto, detail);
            _context.details.Update(detail);
            _context.SaveChanges();

            return CommonService.SuccessResult(detail);
        }

       
    }
}
