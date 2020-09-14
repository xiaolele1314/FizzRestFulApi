using System;
using System.Collections.Generic;
using System.Text;

using Fizz.SalesOrder.Interface;
using Microsoft.AspNetCore.Mvc;
using Fizz.SalesOrder.Service;

namespace Test.MockService
{
    class MockOrderUserService : IOrderUserService
    {
        public IActionResult DeleteDetailByUser()
        {
            return CommonService.SuccessResult("success");
        }

        public IActionResult QueryDetailByUser(int? pageSize, int? pageNum)
        {
            return CommonService.SuccessResult("success");
        }

        public IActionResult QureyOrderByUser(int? pageSize, int? pageNum)
        {
            return CommonService.SuccessResult("success");
        }
    }
}
