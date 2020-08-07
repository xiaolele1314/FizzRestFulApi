using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Fizz.SalesOrder.Models;
using Fizz.SalesOrder.Service;

namespace Fizz.SalesOrder.Controllers
{
    
    [Produces("application/json")]
    [Route("Fizz/Sales/Order")]
    public class OrderController : ControllerBase
    {

        
        private readonly OrderContext _context;

        private readonly IOrderService _orderService;


        public OrderController(OrderContext context, IOrderService service)
        {
            this._context = context;
            this._orderService = service;
        }



        // 创建用户，添加销售订单
        // POST api/order
        [HttpPost()]
        public ResultMes Post([FromHeader] string userName, [FromBody] Order order)
        {
            return _orderService.CreatOrder(userName, order);
        }

        //查询用户所有销售订单
        // GET api/order
        [HttpGet()]
        public object Get([FromHeader] string userName, [FromQuery]string orderNo, [FromQuery] int findType, [FromQuery] int pageSize, [FromQuery] int pageNum)
        {
            switch(findType)
            {
                case (int)GetTypeEnum.GetAll:
                    return _orderService.QueryOrderAll(userName, pageSize, pageNum);
                case (int)GetTypeEnum.GetByKey:
                    return _orderService.QueryOrderByKey(userName, orderNo);
                case (int)GetTypeEnum.GetByUser:
                    return _orderService.QureyOrderByUser(userName, pageSize, pageNum);
                default:
                    return new ResultMes
                    {
                        Code = 202,
                        Message = "don't set findType"
                    };
            }
            
        }

 

        //删除用户所有订单和订单明细
        // DELETE api/order
        [HttpDelete()]
        public ResultMes DeleteUser([FromHeader] string userName, [FromQuery]string orderNo, [FromQuery] int deleteType)
        {
            switch(deleteType)
            {
                case (int)DeleteTypeEnum.DeleteAll:
                    return _orderService.DeleteOrderAll(userName);
                case (int)DeleteTypeEnum.DeleteByKey:
                    return _orderService.DeleteOrderByKey(userName, orderNo);
                case (int)DeleteTypeEnum.DeleteByUser:
                    return _orderService.DeleteOrderByUser(userName);
                default:
                    return new ResultMes
                    {
                        Code = 202,
                        Message = "don't set deleteType"
                    };
            }
            
        }


        //更新用户的销售单
        // PUT api/order
        [HttpPut("{orderNo:int:length(1,10)}")]
        public ResultMes Put([FromHeader] string userName,string orderNo, [FromBody] Order order)
        { 
            return _orderService.UpdateOrder(userName, orderNo, order);
        }

        
    }
    
}
