using Fizz.SalesOrder.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

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
    }
}
