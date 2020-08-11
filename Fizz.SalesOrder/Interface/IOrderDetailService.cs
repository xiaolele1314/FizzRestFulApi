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
        
        PageData<OrderDetail> QueryDetailByUser(string userName, int pageSize, int pageNum);
        PageData<OrderDetail> QueryDetailByKey(int detailNo);
        PageData<OrderDetail> QueryDetailByOrder(string orderNo, int pageSize, int pageNum);

        ResultMessage<OrderDetail> DeleteDetailByKey(int detailNo);
        ResultMessage<OrderDetail> DeleteDetailByUser(string userName);
        ResultMessage<OrderDetail> DeleteDetailByOrder(string orderNo);
        //ResultMes UpdateOrder(string name, string orderNo, Order order);
        ResultMessage<OrderDetail> UpdateOrderDetail(string userName, string orderNo, int detailNo, OrderDetail detail);
    }
}
