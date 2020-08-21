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
    [Route("Fizz/SalesOrder/{orderNo}/OrderDetail/")]
    public class OrderDetailController: ControllerBase
    {


        private readonly IOrderDetailService _detailService;

        public OrderDetailController(IOrderDetailService service)
        {
            
            this._detailService = service;
        }


        // 创建一个明细
        // POST Fizz/SalesOrder/{orderNo}/OrderDetail/
        [HttpPost()]
        public IActionResult Post([FromBody] OrderDetail detail, string orderNo)
        { 
            return _detailService.CreateDetail(detail, orderNo);
          
        }

        //查询一个明细
        // GET Fizz/SalesOrder/{orderNo}/OrderDetail/{detailNO}
        [HttpGet("{detailNo:int}")]
        public IActionResult Get(string orderNo, int detailNo)
        {
            return _detailService.QueryDetail(orderNo,detailNo);
        }

        //查询一个订单下的所有明细
        //GET Fizz/SalesOrder/{orderNo}/OrderDetail/
        [HttpGet("")]
        public IActionResult Get(string orderNo, [FromQuery] int? pageSize, [FromQuery] int? pageNum)
        {
            return _detailService.QueryDetail(orderNo, pageSize, pageNum);
        }

        //删除一个明细
        // DELETE Fizz/SalesOrder/{orderNo}/OrderDetail/{detailNo}
        [HttpDelete("{detailNo:int:length(1,10)}")]
        public IActionResult Delete(string orderNo, int detailNo)
        {

            return _detailService.DeleteDetail(orderNo, detailNo);
        }

        //删除一个订单下的所有明细
        //DELETE Fizz/SalesOrder/{orderNo}/OrderDetail/
        [HttpDelete("")]
        public IActionResult Delete(string orderNo)
        {
             return _detailService.DeleteDetail(orderNo);

        }

        //更新一个明细
        // PUT Fizz/SalesOrder/{orderNo}/OrderDetail/
        [HttpPut("{detailNo:int:length(1,10)}")]
        public IActionResult Put(string orderNo, int detailNo, [FromBody] OrderDetailDto detailDto)
        {
           return _detailService.UpdateDetail(orderNo, detailNo, detailDto);
        }

    }
}
