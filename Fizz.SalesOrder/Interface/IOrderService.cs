using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fizz.SalesOrder.Models;

namespace Fizz.SalesOrder.Service
{
    public interface IOrderService
    {
        ResultMessage<Order> CreatOrder(string userName, Order order);
        //ResultMes CreatOrderDetails(string name, OrderDetail orderDetail, string orderNo);
        PageData<Order> QueryOrderAll(string name, string sortName, MultipleGetStyleOption getStyleOption, int pageSize, int pageNum);
        object QueryOrderByKey(string orderNo);
        PageData<Order> QureyOrderByUser(string userName, int pageSize, int pageNum);
        //List<PageData<OrderDetail>> QueryUserAllOrderDetail(string name, int pageSize, int pageNum);
        
        ResultMessage<Order> DeleteOrderByKey(string userName, string orderNo);
        
        //ResultMes DeleteOrderDetailById(string name, string orderNo);
        ResultMessage<Order> UpdateOrder(string userName, string orderNo, Order order);
        //ResultMes UpdateOrderDetail(string name, string orderNo, int detailNo, OrderDetail detail);

    }
}
