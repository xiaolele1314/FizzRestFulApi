using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Fizz.SalesOrder.Models;
using Fizz.SalesOrder.Service;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;
using Microsoft.AspNetCore.Http;
using Fizz.SalesOrder.Interface;

namespace Fizz.SalesOrder.Controllers
{

    [Produces("application/json")]
    [Route("Fizz/SalesOrder/")]
    public class OrderController : ControllerBase
    {

        private readonly IOrderService _orderService;

        public OrderController(IOrderService service)
        {
            this._orderService = service;
        }

        // 创建一个订单
        // POST Fizz/SalesOrder/
        [HttpPost()]
        public IActionResult Post([FromBody] Order order)
        {
            return _orderService.CreatOrder(order);
        }

        //查询所有订单
        // GET Fizz/SalesOrder/
        [HttpGet("")]
        public IActionResult Get([FromQuery]MultipleGetStyleOption getStyleOption)
        {
            return _orderService.QueryOrder(getStyleOption);
        }

        //查询一个订单
        //Get Fizz/SalesOrder/{orderNo}
        [HttpGet("{orderNo}")]
        public IActionResult Get(string orderNo)
        {
            return _orderService.QueryOrder(orderNo);
        }

        //删除一个订单
        // DELETE Fizz/SalesOrder/
        [HttpDelete("{orderNo}")]
        public IActionResult Delete(string orderNo)
        {         
           return _orderService.DeleteOrder(orderNo);
        }


        // 更新一个订单
        // PUT Fizz/SalesOrder/{orderNo}
        [HttpPut("{orderNo}")]
        public IActionResult Put(string orderNo, [FromBody] OrderDto orderDto)
        { 
            return _orderService.UpdateOrder(orderNo, orderDto);
        }

        
    }
    
}
