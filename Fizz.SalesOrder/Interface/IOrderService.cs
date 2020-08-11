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
        object QueryOrderAll(string name, MultipleGetStyleOption getStyleOption);
        object QueryOrderByKey(string orderNo);   
        ResultMessage<Order> DeleteOrderByKey(string userName, string orderNo);
        ResultMessage<Order> UpdateOrder(string userName, string orderNo, Order order);

    }
}
