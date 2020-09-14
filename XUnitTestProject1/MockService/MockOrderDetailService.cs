using System;
using System.Collections.Generic;
using System.Text;
using Fizz.SalesOrder.Models;
using Fizz.SalesOrder.Service;
using Microsoft.AspNetCore.Mvc;

namespace Test.MockService
{
    class MockOrderDetailService : IOrderDetailService
    {
        public IActionResult CreateDetail(OrderDetail orderDetail, string orderNo)
        {
            return CommonService.SuccessResult("success");
        }

        public IActionResult DeleteDetail(string orderNo, int detailNo)
        {
            return CommonService.SuccessResult("success");
        }

        public IActionResult DeleteDetail(string orderNo)
        {
            return CommonService.SuccessResult("success");
        }

        public IActionResult QueryDetail(string orderNo, int detailNo)
        {
            return CommonService.SuccessResult("success");
        }

        public IActionResult QueryDetail(string orderNo, int? pageSize, int? pageNum)
        {
            return CommonService.SuccessResult("success");
        }

        public IActionResult UpdateDetail(string orderNo, int detailNo, OrderDetailDto detailDto)
        {
            return CommonService.SuccessResult("success");
        }
    }
}
