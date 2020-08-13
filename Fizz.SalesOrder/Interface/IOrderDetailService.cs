using Fizz.SalesOrder.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fizz.SalesOrder.Service
{
    public interface IOrderDetailService
    {
        IActionResult CreateDetail(OrderDetail orderDetail, string orderNo);
        IActionResult QueryDetail(string orderNo, int detailNo);
        IActionResult QueryDetail(string orderNo, int? pageSize, int? pageNum);
        IActionResult DeleteDetail(string orderNo, int detailNo);
        IActionResult DeleteDetail(string orderNo);
        IActionResult UpdateDetail(string orderNo, int detailNo, OrderDetail detail);
    }
}
