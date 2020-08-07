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
        ResultMes CreatOrderDetails(string userName, OrderDetail orderDetail, string orderNo);
        
        PageData<OrderDetail> QueryDetailByUser(string userName, int pageSize, int pageNum);
        PageData<OrderDetail> QueryDetailAll(int pageSize, int pageNum);
        PageData<OrderDetail> QueryDetailByKey(string userName, int detailNo, int pageSize, int pageNum);
        PageData<OrderDetail> QueryDetailByOrder(string userName, string orderNo, int pageSize, int pageNum);

        ResultMes DeleteDetailAll();
        ResultMes DeleteDetailByKey(string userName, int detailNo);
        ResultMes DeleteDetailByUser(string userName);
        ResultMes DeleteDetailByOrder(string userName, string orderNo);
        //ResultMes UpdateOrder(string name, string orderNo, Order order);
        ResultMes UpdateOrderDetail(string userName, string orderNo, int detailNo, OrderDetail detail);
    }
}
