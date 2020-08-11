using Fizz.SalesOrder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fizz.SalesOrder.Interface
{
    public interface IOrderUserService
    {
        object QureyOrderByUser(string userName, int? pageSize, int? pageNum);
        object QueryDetailByUser(string userName, int? pageSize, int? pageNum);
        ResultMessage<OrderDetail> DeleteDetailByUser(string userName);
    }
}
