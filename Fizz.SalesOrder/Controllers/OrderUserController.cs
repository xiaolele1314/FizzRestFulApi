using Fizz.SalesOrder.Interface;
using Fizz.SalesOrder.Models;
using Fizz.SalesOrder.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fizz.SalesOrder.Controllers
{

    [Produces("application/json")] 
    [Route("Fizz/")]
    public class OrderUserController : ControllerBase
    {
        private readonly IOrderUserService _orderUserService;

        public OrderUserController(SalesContext context, IOrderUserService service)
        {
            this._orderUserService = service;
        }

        //获取用户下的所有订单
        //GET Fizz/OrderUser
        [HttpGet("OrderUser")]
        public IActionResult GetOrder([FromQuery] int? pageSize, [FromQuery] int? pageNum)
        {
            return _orderUserService.QureyOrderByUser(pageSize, pageNum);
        }

        //删除用户下的所有明细
        //DELETE Fizz/DetailUser
        [HttpDelete("DetailUser")]
        public IActionResult DeletOrdere()
        {
            return _orderUserService.DeleteDetailByUser();
        }

        //获取用户下的所有明细
        //GET Fizz/DetailUser
        [HttpGet("DetailUser")]
        public IActionResult GetDetail([FromQuery] int? pageSize, [FromQuery] int? pageNum)
        {
            return _orderUserService.QueryDetailByUser(pageSize, pageNum);
        }
    }
}
