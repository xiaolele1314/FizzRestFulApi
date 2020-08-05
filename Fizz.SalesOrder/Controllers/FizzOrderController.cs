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
    [Route("api/order")]
    public class FizzOrderController : ControllerBase
    {

        //private readonly string noRegex = "^\\d{{1,10}}$";
        private readonly OrderContext _context;

        private readonly IOrderService _orderService;


        public FizzOrderController(OrderContext context, IOrderService service)
        {
            this._context = context;
            _orderService = service;
        }

   

        // 创建用户，添加销售订单
        // POST api/order
        [HttpPost]
        public ResultMes Post([FromQuery] string name, [FromBody] OrderOrDetail body,  [FromQuery] string orderNo, [FromQuery] int insertType)
        {
            switch(insertType)
            {
                case 1:
                    return _orderService.CreatOrderDetails(body.Detail, orderNo, name);
                case 2:
                    return _orderService.CreatOrder(name, body.Order);
                default:
                    return new ResultMes
                    {
                        Code = 202,
                        Message = "don't set insertType"
                    };
            }
        }

        //查询用户所有销售订单
        // GET api/order
        [HttpGet]
        public object Get([FromQuery] string name, [FromQuery] string orderNo, [FromQuery] int findType, [FromQuery] int pageSize, [FromQuery] int pageNum)
        {
            switch(findType)
            {
                case 1:
                    return _orderService.QueryUserAllOrder(name,pageSize,pageNum);
                case 2:
                    return _orderService.QueryUserOrder(name, orderNo,pageSize,pageNum);
                case 3:
                    return _orderService.QueryUserAllOrderDetail(name,pageSize,pageNum);
                default:
                    return new ResultMes
                    {
                        Code = 202,
                        Message = "don't set findType"
                    };
            }
            
        }

        ////查询用户某个销售订单
        //// GET api/order/id
        //[HttpGet("{orderNo:int:length(1,10)}")]
        //public object GetId(string orderNo, [FromQuery] string name)
        //{
        //    return _orderService.QueryUserOrder(name, orderNo);
        //}

        ////查询用户所有订单明细
        ////GET api/order/details
        //[HttpGet("details")]
        //public object GetDetails([FromQuery] string name)
        //{

        //    return _orderService.QueryUserAllOrderDetail(name);
        //}

        //删除用户所有订单和订单明细
        // DELETE api/order
        [HttpDelete()]
        public ResultMes DeleteUser([FromQuery] string name, [FromQuery] string orderNo, [FromQuery] int deleteType)
        {
            switch(deleteType)
            {
                case 1:
                    return _orderService.DeleteUserAll(name);
                case 2:
                    return _orderService.DeleteUserOrder(name, orderNo);
                case 3:
                    return _orderService.DeleteOrderDetailById(name, orderNo);
                default:
                    return new ResultMes
                    {
                        Code = 202,
                        Message = "don't set deleteType"
                    };
            }
            
        }

        ////删除用户某个订单和订单明细
        //// DELETE api/order/id
        //[HttpDelete("{orderNo:int:length(1,10)}")]
        //public ResultMes Delete(string orderNo, [FromQuery] string name)
        //{

        //    return _orderService.DeleteUserOrder(name, orderNo);
        //}

        ////删除用户某个销售单的明细
        //// DELETE api/order/id/details
        //[HttpDelete("{orderNo:int:length(1,10)}/details")]
        //public ResultMes DeleteDetails(string orderNo, [FromQuery] string name)
        //{
        //    return _orderService.DeleteOrderDetailById(name, orderNo);
        //}

        //更新用户的销售单
        // PUT api/order
        [HttpPut()]
        public ResultMes Put([FromQuery] string name,[FromQuery] string orderNo, [FromQuery] int detailNo, [FromBody] OrderOrDetail body, [FromQuery] int updateType)
        { 
            switch (updateType)
            {
                case 1:
                    return _orderService.UpdateOrder(name, orderNo, body.Order);
                case 2:
                    return _orderService.UpdateOrderDetail(name, orderNo, detailNo, body.Detail);
                default:
                    return new ResultMes
                    {
                        Code = 202,
                        Message = "don't set updateType"
                    };
            }
        }

        ////更新用户的明细单
        //// PUT api/order/id/details/d_id
        //[HttpPut("{orderNo:int:length(1,10)}/details/{detailNo:int:length(1,10)}")]
        //public ResultMes PutDetails([FromQuery] string name, string orderNo, int detailNo, [FromBody] OrderDetail detail)
        //{
        //    return _orderService.UpdateOrderDetail(name, orderNo, detailNo, detail);
        //}

      

        
    }
}
