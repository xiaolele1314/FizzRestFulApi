using Fizz.SalesOrder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fizz.SalesOrder.Service
{
    public interface IOrderDetailService
    {
        //ResultMes CreatOrder(string name, Order order);
        ResultMessage<OrderDetail> CreatOrderDetails(string userName, OrderDetail orderDetail, string orderNo);        
        object QueryDetailByKey(string orderNo, int detailNo);
        object QueryDetailByOrder(string orderNo, int? pageSize, int? pageNum);

        ResultMessage<OrderDetail> DeleteDetailByKey(string orderNo, int detailNo);
        ResultMessage<OrderDetail> DeleteDetailByOrder(string orderNo);
        //ResultMes UpdateOrder(string name, string orderNo, Order order);
        ResultMessage<OrderDetail> UpdateOrderDetail(string userName, string orderNo, int detailNo, OrderDetail detail);
    }
}
