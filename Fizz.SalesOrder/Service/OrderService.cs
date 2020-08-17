using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
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

        

        public OrderService(SalesContext context)
        {
            this._context = context;
            
        }

        public IActionResult CreatOrder(Order order)
        {
            if(order.No == null)
            {
                return new JsonResult("未设置订单编号") { StatusCode = StatusCodes.Status400BadRequest};
            }
            
        
            if(_context.orders.AsNoTracking().Where(o => o.No == order.No).Count() > 0)
            {
                return new JsonResult("订单编号已经存在") { StatusCode = StatusCodes.Status400BadRequest};
            }

            _context.Add(order);
            _context.SaveChanges();

            return new JsonResult(order){ StatusCode = StatusCodes.Status200OK };
        }
          
        public IActionResult DeleteOrder(string orderNo)
        {

            //判断订单是否存在
            var order = _context.orders.Find(orderNo);
            if (order == null)
            {
                return new JsonResult("订单不存在") { StatusCode = StatusCodes.Status400BadRequest };
            }

            //只删除pending数据
            if (order.Status != (int)OrderStatusEnum.Pending)
            {
                return new JsonResult("订单状态不是pending，不能删除") { StatusCode = StatusCodes.Status400BadRequest };
            }

            //删除销售单和明细单
            _context.orders.Remove(order);

            _context.SaveChanges();

            return new JsonResult("") { StatusCode = StatusCodes.Status200OK };
        }

       
        public IActionResult QueryOrder(MultipleGetStyleOption getStyleOption)
        {           
            var results = CommonService.SelfQueryWhere(_context.orders.AsNoTracking(), getStyleOption);

            return results;
            
        }

        public IActionResult QueryOrder(string orderNo)
        {
            var order = _context.orders.AsNoTracking().Where(o => o.No == orderNo).FirstOrDefault();

            return new JsonResult(order) { StatusCode = StatusCodes.Status200OK };
        }

        public IActionResult UpdateOrder(string orderNo, Order order)
        {

            if (order.No != null || order.CreateUserNo != null || order.CreateUserDate != DateTime.MinValue)
            {
                return new JsonResult("不能修改编号、创建人编号、创建日期") { StatusCode = StatusCodes.Status400BadRequest };
            }

            //判断数据是否存在
            var oldOrder = _context.orders.AsNoTracking().Where(o => o.No == orderNo).FirstOrDefault();
            if (oldOrder == null)
            {
                return new JsonResult("订单不存在") { StatusCode = StatusCodes.Status400BadRequest };
            }

            //更新销售订单数据
            DateTime now = System.DateTime.Now;
            order.No = orderNo;

            
            order.UpdateChangedField<Order>(oldOrder);

            var u = _context.orders.Update(order);
            _context.SaveChanges();

            return new JsonResult(order) { StatusCode = StatusCodes.Status200OK };
        }
    }
}
