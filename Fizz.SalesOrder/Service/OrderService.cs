using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Fizz.SalesOrder.Extensions;
using Fizz.SalesOrder.Interface;
using Fizz.SalesOrder.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Fizz.SalesOrder.Service
{
    public class OrderService : IOrderService
    {
        private readonly SalesContext _context;

        private readonly IMapper _mapper;

        public OrderService(SalesContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public IActionResult CreatOrder(Order order)
        {
            if(order.No == null)
            {
                return CommonService.FailResult("未设置订单编号");
            }
            
        
            if(_context.orders.AsNoTracking().Where(o => o.No == order.No).Count() > 0)
            {
                return CommonService.FailResult("订单编号已经存在");
            }

            _context.Add(order);
            _context.SaveChanges();

            return CommonService.SuccessResult(order);
        }
          
        public IActionResult DeleteOrder(string orderNo)
        {

            //判断订单是否存在
            var order = _context.orders.Find(orderNo);
            if (order == null)
            {
                return CommonService.FailResult("订单不存在");
            }

            //只删除pending数据
            if (order.Status != (int)OrderStatusEnum.Pending)
            {
                return CommonService.FailResult("订单状态不是pending，不能删除");
            }

            //删除销售单和明细单
            _context.orders.Remove(order);

            _context.SaveChanges();

            return CommonService.SuccessResult("");
        }

       
        public IActionResult QueryOrder(MultipleGetStyleOption getStyleOption)
        {           
            return _context.orders.AsNoTracking().FindByOptions(getStyleOption);   
        }

        public IActionResult QueryOrder(string orderNo)
        {
            var order = _context.orders.AsNoTracking().Where(o => o.No == orderNo).FirstOrDefault();

            return CommonService.SuccessResult(order);
        }

        public IActionResult UpdateOrder(string orderNo, OrderDto orderDto)
        {

            if (orderDto.No != null || orderDto.CreateUserNo != null || orderDto.CreateUserDate != DateTime.MinValue)
            {
                return CommonService.FailResult("不能修改编号、创建人编号、创建日期");
            }

            //判断数据是否存在
            var order = _context.orders.AsNoTracking().Where(o => o.No == orderNo).FirstOrDefault();
            if (order == null)
            {
                return CommonService.FailResult("订单不存在");
            }

            //更新销售订单数据
            DateTime now = System.DateTime.Now;
            orderDto.No = orderNo;


            //order.UpdateChangedField<Order>(oldOrder);

            _mapper.Map(orderDto, order);

            var u = _context.orders.Update(order);
            _context.SaveChanges();

            return CommonService.SuccessResult(order);
        }
    }
}
