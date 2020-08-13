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
        public static List<User> InitializeUsers(IServiceCollection services)
        {
            IServiceProvider provider = services.BuildServiceProvider();
            SalesContext orderContext = provider.GetService<SalesContext>();


            List<User> users = new List<User> { new User { Name = "fizz" }, new User { Name = "s52" } };

            //查询数据库为user添加no
            foreach (User user in users)
            {
                var orders = orderContext.orders.Where(o => o.CreateUserNo == user.Name).AsNoTracking();
                foreach (Order order in orders)
                {
                    if (!user.OrderNos.Contains(order.No))
                        user.OrderNos.Add(order.No);
                }
            }

            return users;
        }

        public static Expression<Func<Order,bool>> AddOrLambda(List<OrderStatusEnum> list)
        {
            List<Expression<Func<Order, bool>>> expressions = new List<Expression<Func<Order, bool>>>();
            foreach (var status in list)
            {
                Expression<Func<Order, bool>> expression = o => o.Status == (int)status;
                expressions.Add(expression);
            }

            var param = Expression.Parameter(typeof(Order), "order");
            Expression body = null;
            foreach (var e in expressions)
            {
                if (body != null)
                {
                    body = Expression.OrElse(body, Expression.Invoke(e, param));
                }
                else
                {
                    body = Expression.Invoke(e, param);
                }

            }
            var lambda = Expression.Lambda<Func<Order, bool>>(body, param);

            return lambda;
        }

        public static IQueryable<Order>  AddFindPage(IQueryable<Order> source, string sortStr)
        {

            string[] options = sortStr.Split(" ");

            string sortName = options[0];
            string sortType = "desc";

            if(options.Length > 1)
            {
                sortType = options[1].ToLower() ?? sortType;
            }

            Type t = typeof(Order);
            PropertyInfo[] pi = t.GetProperties();
            PropertyInfo info = pi.Where(o => o.Name.ToLower() == sortName.ToLower()).FirstOrDefault();

            if(info == null)
            {
                source = source.OrderByDescending(o => o.CreateUserDate);
            }
            else
            {
                ParameterExpression param = Expression.Parameter(typeof(Order), "o");               
                MemberExpression s = Expression.Property(param, typeof(Order).GetProperty(info.Name, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance));
                var lambda = Expression.Lambda<Func<Order, object>>(Expression.Convert(s,typeof(object)), param);

                if(sortType == "asc")
                {
                    source = source.OrderBy(lambda);
                }
                else
                {
                    source = source.OrderByDescending(lambda);
                }
               
            }
            
            return source;
        }

        public static IActionResult SelfQueryWhere(IQueryable<Order> source, MultipleGetStyleOption getStyleOption)
        {
            //范围查询
            source = source.Where(o => o.SignDate >= getStyleOption.SignDateRange.DateMin && o.SignDate <= getStyleOption.SignDateRange.DateMax);
            source = source.Where(o => o.CreateUserDate >= getStyleOption.CreateOrderDateRange.DateMin && o.CreateUserDate <= getStyleOption.CreateOrderDateRange.DateMax);
            source = source.Where(o => o.UpdateUserDate >= getStyleOption.UpdateOrderDateRange.DateMin && o.UpdateUserDate <= getStyleOption.UpdateOrderDateRange.DateMax);

            //状态查询
            source = source.Where(AddOrLambda(getStyleOption.Status.ToList()));

            //模糊查询
            if (getStyleOption.OrderNo != null)
            {
                source = source.Where(o => o.No.Contains(getStyleOption.OrderNo));
            }

            if (getStyleOption.ClientName != null)
            {
                source = source.Where(o => o.ClientName.Contains(getStyleOption.ClientName));
            }

            if (getStyleOption.Comment != null)
            {
                source = source.Where(o => o.Comment.Contains(getStyleOption.Comment));
            }

            //排序
            source = AddFindPage(source, getStyleOption.SortStr);

            //分页
            int count = source.Count();

            PageData<Order> pageData = null;

            if(count > 0)
            {
                decimal pageCount = Math.Ceiling((decimal)((decimal)count / getStyleOption.PageSize));

                if (getStyleOption.PageNum > pageCount || getStyleOption.PageNum <= 0)
                {
                    return new JsonResult("pageNum设置错误") { StatusCode = StatusCodes.Status400BadRequest };
                }
                source = source.Skip((int)(getStyleOption.PageSize * (getStyleOption.PageNum - 1))).Take((int)getStyleOption.PageSize);

                pageData = new PageData<Order> { PageCount = (int)pageCount, PageNum = getStyleOption.PageNum, PageItems = source.ToList() };
            }
            

            
            return new JsonResult(pageData) { StatusCode = StatusCodes.Status200OK };
        }



    }
}
