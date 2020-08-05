using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 

namespace Fizz.SalesOrder.Service
{
    public class FizzSaleExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if(context.ExceptionHandled == false)
            {
                context.Result = new ContentResult
                {
                    Content = context.Exception.Message,
                    StatusCode = StatusCodes.Status400BadRequest,
                    ContentType = "text/html;charset=utf-8"
                };
            }
            context.ExceptionHandled = true;
        }
    }
}
