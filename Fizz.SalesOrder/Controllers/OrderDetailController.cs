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
    [Route("Fizz/SalesOrder/{orderNo:int:length(1,10)}/OrderDetail/")]
    public class OrderDetailController:ControllerBase
    {
        private readonly SalesContext _context;

        private readonly IOrderDetailService _detailService;


        public OrderDetailController(SalesContext context, IOrderDetailService service)
        {
            this._context = context;
            this._detailService = service;
        }


        // 创建用户，添加销售订单
        // POST api/order
        [HttpPost()]
        public ResultMessage<OrderDetail> Post([FromHeader] string userName, [FromBody] OrderDetail detail, string orderNo)
        { 
            return _detailService.CreatOrderDetails(userName, detail, orderNo);
          
        }

        //查询用户所有销售订单
        // GET api/order

        [HttpGet("{detailNo:int}")]
        public object Get(string orderNo, int detailNo)
        {
            return _detailService.QueryDetailByKey(orderNo,detailNo);
        }

        [HttpGet("")]
        public object Get(string orderNo, [FromQuery] int? pageSize, [FromQuery] int? pageNum)
        {
            pageNum = pageNum ?? 1;
            pageSize = pageSize ?? 100;
            return _detailService.QueryDetailByOrder(orderNo, pageSize, pageNum);
        } 

        //删除用户所有订单和订单明细
        // DELETE api/order

        [HttpDelete("{detailNo:int:length(1,10)}")]
        public ResultMessage<OrderDetail> DeleteUser(string orderNo, int detailNo)
        {

            return _detailService.DeleteDetailByKey(orderNo, detailNo);
        }

        

        [HttpDelete("")]
        public ResultMessage<OrderDetail> DeleteByOrderNo(string orderNo)
        {
             return _detailService.DeleteDetailByOrder(orderNo);

        }

        //更新用户的销售单
        // PUT api/order
        [HttpPut("{detailNo:int:length(1,10)}")]
        public ResultMessage<OrderDetail> Put([FromHeader] string userName, string orderNo, int detailNo, [FromBody] OrderDetail detail)
        {
           return _detailService.UpdateOrderDetail(userName, orderNo, detailNo, detail);
        }

    }
}
