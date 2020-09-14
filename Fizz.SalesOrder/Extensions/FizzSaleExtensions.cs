using Fizz.SalesOrder.Models;
using Fizz.SalesOrder.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Fizz.SalesOrder.Extensions
{
    public static class FizzSaleExtensions
    {
        public static void SetCommonValue(this SalesCommonBase salesOption, DateTime createSaleDate, string createSaleNo, DateTime updateSaleDate, string updateSaleNo)
        {
            salesOption.CreateUserDate = createSaleDate;
            salesOption.CreateUserNo = createSaleNo;
            salesOption.UpdateUserDate = updateSaleDate;
            salesOption.UpdateUserNo = updateSaleNo;

        }

        //获取要更新列，通过赋值只更新改变的字段
        public static T UpdateChangedField<T>(this T bodyObject, T dbObject) where T : new()
        {
            T newObject = new T();
            Type t = newObject.GetType();
            PropertyInfo[] pi = t.GetProperties();

            int len = pi.Length;
            for (int i = 0; i < len; i++)
            {
                var ob_new = pi[i].GetValue(bodyObject);
                var ob = pi[i].GetValue(newObject);
                var ob_old = pi[i].GetValue(dbObject);
                if (ob_new == null || ob_new.Equals(ob))
                {
                    pi[i].SetValue(bodyObject, ob_old);
                }
            }

            return bodyObject;
        }

        //automapper更新改变字段
        public static T UpdateChangedFieldByAutoMapper<T>(this T source, T destination)
        {

            return destination;
        }

        //扩展分页方法
        public static IActionResult PageSales<T>(this IQueryable<T> source, int? pageSize, int? pageNum)
        {
            PageData<T> pageData = null;
            if (source.Count() > 0)
            {
                int pageCount = (int)Math.Ceiling((decimal)((decimal)source.Count() / pageSize));

                if (pageNum > pageCount || pageNum <= 0)
                {
                    return CommonService.FailResult("pageNum错误");
                }

                //分页查询            
                source = source.Skip((int)(pageSize * (pageNum - 1))).Take((int)pageSize);

                if(source.Count() == 0)
                {
                    return CommonService.FailResult("查询不到数据");
                }

                pageData = new PageData<T> { PageCount = (int)pageCount, PageNum = pageNum, PageItems = source.ToList() };
            }
            else
            {
                return CommonService.FailResult("查询不到数据");
            }
            

            return CommonService.SuccessResult(pageData);
        }

        //扩展参数查询方法
        public static IActionResult FindByOptions(this IQueryable<Order> source, MultipleGetStyleOption getStyleOption)
        {
            //范围查询
            source = source.Where(o => o.SignDate >= getStyleOption.SignDateRange.DateMin && o.SignDate <= getStyleOption.SignDateRange.DateMax);
            source = source.Where(o => o.CreateUserDate >= getStyleOption.CreateOrderDateRange.DateMin && o.CreateUserDate <= getStyleOption.CreateOrderDateRange.DateMax);
            source = source.Where(o => o.UpdateUserDate >= getStyleOption.UpdateOrderDateRange.DateMin && o.UpdateUserDate <= getStyleOption.UpdateOrderDateRange.DateMax);

            //状态查询
            source = source.Where(ListLambda(getStyleOption.Status.ToList()));

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
            source = SortSales(source, getStyleOption.SortStr);

            //分页
            return source.PageSales<Order>(getStyleOption.PageSize, getStyleOption.PageNum);
        }

        //扩展排序方法
        public static IQueryable<Order> SortSales(this IQueryable<Order> source, string sortStr)
        {

            string[] options = sortStr.Split(" ");

            string sortName = options[0];
            string sortType = "desc";

            if (options.Length > 1)
            {
                sortType = options[1].ToLower() ?? sortType;
            }

            Type t = typeof(Order);
            PropertyInfo[] pi = t.GetProperties();
            PropertyInfo info = pi.Where(o => o.Name.ToLower() == sortName.ToLower()).FirstOrDefault();

            if (info == null)
            {
                source = source.OrderByDescending(o => o.CreateUserDate);
            }
            else
            {
                ParameterExpression param = Expression.Parameter(typeof(Order), "o");
                MemberExpression s = Expression.Property(param, typeof(Order).GetProperty(info.Name, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance));
                var lambda = Expression.Lambda<Func<Order, object>>(Expression.Convert(s, typeof(object)), param);

                if (sortType == "asc")
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

        //扩展list表达式树
        public static Expression<Func<Order, bool>> ListLambda(this List<OrderStatusEnum> list)
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
    }
}
