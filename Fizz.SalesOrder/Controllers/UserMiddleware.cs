using Fizz.SalesOrder.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fizz.SalesOrder.Controllers
{
    public class UserMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var userService = context.RequestServices.GetRequiredService<IUserService>();
            var userName = context.Request.Headers["userName"];
            userService.setUser(userName);
            await next.Invoke(context);
        }
    }
}
