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


        private readonly SalesContext _context;

        private readonly IOrderUserService _orderUserService;

        public OrderUserController(SalesContext context, IOrderUserService service)
        {
            this._context = context;
            this._orderUserService = service;
        }

        [HttpGet("OrderUser")]
        public object GetByUser([FromHeader] string userName, [FromQuery] int? pageSize, [FromQuery] int? pageNum)
        {
            pageSize = pageSize ?? 100;
            pageNum = pageNum ?? 1;
            return _orderUserService.QureyOrderByUser(userName, pageSize, pageNum);
        }

        [HttpDelete("DetailUser")]
        public ResultMessage<OrderDetail> DeleteUser([FromHeader] string userName)
        {

            return _orderUserService.DeleteDetailByUser(userName);

        }

        [HttpGet("DetailUser")]
        public object Get([FromHeader] string userName, [FromQuery] int? pageSize, [FromQuery] int? pageNum)
        {
            pageSize = pageSize ?? 100;
            pageNum = pageNum ?? 1;
            return _orderUserService.QueryDetailByUser(userName, pageSize, pageNum);
        }



    }
}
