using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fizz.SalesOrder.Service
{
    public class CommonService
    {
        public static IActionResult SuccessResult(object value)
        {
            JsonResult result = new JsonResult(value) { StatusCode = StatusCodes.Status200OK };
            return result;
        }

        public static IActionResult FailResult(object value)
        {
            JsonResult result = new JsonResult(value) { StatusCode = StatusCodes.Status400BadRequest };
            return result;
        }

        public int test(int a)
        {
            return a;
        }
    }
}
