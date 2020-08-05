using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fizz.SalesOrder.Models;

namespace Fizz.SalesOrder.Service
{
    public interface IOrderService
    {
        ResultMes CreatOrder(string name, Order order);
        ResultMes CreatOrderDetails(OrderDetail orderDetail, string orderNo, string name);
        List<PageData<Order>> QueryUserAllOrder(string name,int pageSize, int pageNum);
        Object QueryUserOrder(string name, string orderNo, int pageSize, int pageNum);
        List<PageData<OrderDetail>> QueryUserAllOrderDetail(string name, int pageSize, int pageNum);
        ResultMes DeleteUserAll(string name);
        ResultMes DeleteUserOrder(string name, string orderNo);
        ResultMes DeleteOrderDetailById(string name, string orderNo);
        ResultMes UpdateOrder(string name, string orderNo, Order order);
        ResultMes UpdateOrderDetail(string name, string orderNo, int detailNo, OrderDetail detail);

    }
}
