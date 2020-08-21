using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fizz.SalesOrder.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fizz.SalesOrder.Service
{
    public interface IOrderService
    {
        IActionResult CreatOrder(Order order);
        IActionResult QueryOrder(MultipleGetStyleOption getStyleOption);
        IActionResult QueryOrder(string orderNo);
        IActionResult DeleteOrder(string orderNo);
        IActionResult UpdateOrder(string orderNo, OrderDto orderDto);

    }
}
