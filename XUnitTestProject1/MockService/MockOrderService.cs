using System;
using System.Collections.Generic;
using System.Text;
using Fizz.SalesOrder.Models;
using Fizz.SalesOrder.Service;
using Microsoft.AspNetCore.Mvc;

namespace Test.MockService
{
    class MockOrderService : IOrderService
    {
        public IActionResult CreatOrder(Order order)
        {
            return CommonService.SuccessResult("success");
        }

        public IActionResult DeleteOrder(string orderNo)
        {
            return CommonService.SuccessResult("success");
        }

        public IActionResult QueryOrder(MultipleGetStyleOption getStyleOption)
        {
            return CommonService.SuccessResult("success");
        }

        public IActionResult QueryOrder(string orderNo)
        {
            return CommonService.SuccessResult("success");
        }

        public IActionResult UpdateOrder(string orderNo, OrderDto orderDto)
        {
            return CommonService.SuccessResult("success");
        }
    }
}
