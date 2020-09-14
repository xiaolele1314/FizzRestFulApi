using Fizz.SalesOrder.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Fizz.SalesOrder.Controllers
{

    [Produces("application/json")] 
    [Route("Fizz/")]
    public class OrderUserController : ControllerBase
    {
        private readonly IOrderUserService _orderUserService;

        public OrderUserController(IOrderUserService service)
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
        public IActionResult DeleteOrderDetail()
        {
            return _orderUserService.DeleteDetailByUser();
        }

        //获取用户下的所有明细
        //GET Fizz/DetailUser
        [HttpGet("DetailUser")]
        public IActionResult GetOrderDetail([FromQuery] int? pageSize, [FromQuery] int? pageNum)
        {
            return _orderUserService.QueryDetailByUser(pageSize, pageNum);
        }
    }
}
