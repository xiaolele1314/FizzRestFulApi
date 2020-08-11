using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Fizz.SalesOrder.Models;
using Fizz.SalesOrder.Service;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;


namespace Fizz.SalesOrder.Controllers
{

    [Produces("application/json")]
    [Route("Fizz/SalesOrder/")]
    public class OrderController : ControllerBase
    {


        private readonly SalesContext _context;

        private readonly IOrderService _orderService;

        public OrderController(SalesContext context, IOrderService service)
        {
            this._context = context;
            this._orderService = service;
        }

        // 创建用户，添加销售订单
        // POST api/order
        [HttpPost()]
        public ResultMessage<Order> Post([FromHeader] string userName, [FromBody] Order order)
        {
            return _orderService.CreatOrder(userName, order);
        }

        //查询用户所有销售订单
        // GET api/order
        [HttpGet("")]
        public object Get([FromHeader] string userName, [FromQuery]MultipleGetStyleOption getStyleOption)
        {
            return _orderService.QueryOrderAll(userName, getStyleOption);
        }

        [HttpGet("{orderNo:int:length(1,10)}")]
        public object Get(string orderNo)
        {
            return _orderService.QueryOrderByKey(orderNo);
        }

        //删除用户所有订单和订单明细
        // DELETE api/order
        [HttpDelete("{orderNo:int:length(1,10)}")]
        public ResultMessage<Order> DeleteUser([FromHeader] string userName, string orderNo)
        {         
           return _orderService.DeleteOrderByKey(userName, orderNo);
        }


        //更新用户的销售单
        // PUT api/order
        [HttpPut("{orderNo:int:length(1,10)}")]
        public ResultMessage<Order> Put([FromHeader] string userName,string orderNo, [FromBody] Order order)
        { 
            return _orderService.UpdateOrder(userName, orderNo, order);
        }

        
    }
    
}
