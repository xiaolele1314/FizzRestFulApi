using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Service
{
    public interface IOrderService
    {
        ResultMes CreatOrder(string name, Order order, OrderContext _context);
        ResultMes CreatOrderDetails(OrderDetail orderDetail, string id, string name, OrderContext _context);
        Object QueryUserAllOrder(string name, OrderContext _context);
        Object QueryUserOrder(string name, string id, OrderContext _context);
        Object QueryUserAllOrderDetail(string name, OrderContext _context);
        ResultMes DeleteUserAll(string name, OrderContext _context);
        ResultMes DeleteUserOrder(string name, string id, OrderContext _context);
        ResultMes DeleteOrderDetailById(string name, string id, OrderContext _context);
        ResultMes UpdateOrder(string name, string id, Order order, OrderContext _context);
        ResultMes UpdateOrderDetail(string name, string id, int d_id, OrderDetail detail, OrderContext _context);

    }
}
