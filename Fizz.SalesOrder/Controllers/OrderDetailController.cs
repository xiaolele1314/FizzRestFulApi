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
    [Route("Fizz/Sales/OrderDetail")]
    public class OrderDetailController:ControllerBase
    {
        private readonly OrderDetailContext _context;

        private readonly IOrderDetailService _detailService;


        public OrderDetailController(OrderDetailContext context, IOrderDetailService service)
        {
            this._context = context;
            this._detailService = service;
        }


        // 创建用户，添加销售订单
        // POST api/order
        [HttpPost("{orderNo:int:length(1,10)}")]
        public ResultMes Post([FromHeader] string userName, [FromBody] OrderDetail detail, string orderNo)
        { 
            return _detailService.CreatOrderDetails(userName, detail, orderNo);
          
        }

        //查询用户所有销售订单
        // GET api/order
        [HttpGet()]
        public object Get([FromQuery] int pageSize, [FromQuery] int pageNum)
        {  
            return _detailService.QueryDetailAll(pageSize, pageNum);
        }

        [HttpGet("order/{detailNo}")]
        public object Get([FromHeader] string userName, int detailNo)
        {
              return _detailService.QueryDetailByKey(userName, detailNo);
        }

        [HttpGet("user")]
        public object Get([FromHeader] string userName, [FromQuery] int pageSize, [FromQuery] int pageNum)
        {
              return _detailService.QueryDetailByUser(userName, pageSize, pageNum);      
        }

        [HttpGet("{orderNo}")]
        public object Get([FromHeader] string userName, string orderNo, [FromQuery] int pageSize, [FromQuery] int pageNum)
        {
              return _detailService.QueryDetailByOrder(userName, orderNo, pageSize, pageNum);
        }

        //删除用户所有订单和订单明细
        // DELETE api/order
        [HttpDelete()]
        public ResultMes DeleteUser()
        {
             return _detailService.DeleteDetailAll();

        }

        [HttpDelete("order/{detailNo}")]
        public ResultMes DeleteUser([FromHeader] string userName, int detailNo)
        {
             return _detailService.DeleteDetailByKey(userName, detailNo);
        }

        [HttpDelete("user")]
        public ResultMes DeleteUser([FromHeader] string userName)
        {

             return _detailService.DeleteDetailByUser(userName);

        }

        [HttpDelete("{orderNo}")]
        public ResultMes DeleteUser([FromHeader] string userName,string orderNo)
        {
             return _detailService.DeleteDetailByOrder(userName, orderNo);

        }

        //更新用户的销售单
        // PUT api/order
        [HttpPut("{orderNo:int:length(1,10)}/{detailNo:int:length(1,10)}")]
        public ResultMes Put([FromHeader] string userName, string orderNo, int detailNo, [FromBody] OrderDetail detail)
        {
            return _detailService.UpdateOrderDetail(userName, orderNo, detailNo, detail);
        }

    }
}
