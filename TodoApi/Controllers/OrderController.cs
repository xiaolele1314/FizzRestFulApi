using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoApi.Service;

namespace TodoApi.Controllers
{
    [Produces("application/json")]
    [Route("api/{name:regex(^\\D{{1,5}})}/order")]
    public class OrderController : ControllerBase
    {
        private readonly OrderContext _context;

        private readonly OrderService _orderService;


        public OrderController(OrderContext context)
        {
            this._context = context;
            _orderService = new OrderService();
        }

   

        // 创建用户，添加销售订单
        // POST api/{name}/order
        [HttpPost]
        public ResultMes Post(string name, [FromBody] Order order)
        {
            return _orderService.CreatOrder(name, order, _context);
        }

        //为用户添加销售
        //POST api/name/order/id/details
        [HttpPost("{id:regex(^\\d{{1,10}}$)}/details")]
        public ResultMes PostDetails([FromBody] OrderDetail orderDetail,string id,string name)
        {

            return _orderService.CreatOrderDetails(orderDetail, id, name, _context);
        }

        //查询用户所有销售订单
        // GET api/name/order
        [HttpGet]
        public object Get(string name)
        {
            return _orderService.QueryUserAllOrder(name, _context);
        }

        //查询用户某个销售订单
        // GET api/name/order/id
        [HttpGet("{id:regex(^\\d{{1,10}}$)}")]
        public object GetId(string id,string name)
        {
            return _orderService.QueryUserOrder(name, id, _context);
        }

        //查询用户所有订单明细
        //GET api/name/order/details
        [HttpGet("details")]
        public object GetDetails(string name)
        {

            return _orderService.QueryUserAllOrderDetail(name, _context);
        }

        //删除用户所有订单和订单明细
        // DELETE api/name/order
        [HttpDelete()]
        public ResultMes DeleteUser(string name)
        {
            return _orderService.DeleteUserAll(name, _context);
        }

        //删除用户某个订单和订单明细
        // DELETE api/name/order/id
        [HttpDelete("{id:regex(^\\d{{1,10}}$)}")]
        public ResultMes Delete(string id,string name)
        {

            return _orderService.DeleteUserOrder(name, id, _context);
        }

        //删除用户某个销售单的明细
        // DELETE api/name/order/id/details
        [HttpDelete("{id:regex(^\\d{{1,10}}$)}/details")]
        public ResultMes DeleteDetails(string id,string name)
        {
            return _orderService.DeleteOrderDetailById(name, id, _context);
        }

        //更新用户的销售单
        // PUT api/name/order/id
        [HttpPut("{id:regex(^\\d{{1,10}}$)}")]
        public ResultMes Put(string name,string id, [FromBody] Order order)
        {
            return _orderService.UpdateOrder(name, id, order, _context);
        }

        //更新用户的明细单
        // PUT api/name/order/id/details/d_id
        [HttpPut("{id:regex(^\\d{{1,10}}$)}/details/{d_id:regex(^\\d{{1,10}}$)}")]
        public ResultMes PutDetails(string name, string id, int d_id, [FromBody] OrderDetail detail)
        {
            return _orderService.UpdateOrderDetail(name, id,d_id,detail, _context);
        }

      

        
    }
}
