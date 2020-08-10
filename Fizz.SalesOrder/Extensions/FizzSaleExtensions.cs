using Fizz.SalesOrder.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Fizz.SalesOrder.Extensions
{
    public static class FizzSaleExtensions
    {
        public static void SetCommonValue(this SalesCommonBase salesOption, DateTime createSaleDate, string createSaleNo, DateTime updateSaleDate, string updateSaleNo)
        {
            salesOption.CreateUserDate = createSaleDate;
            salesOption.CreateUserNo = createSaleNo;
            salesOption.UpdaeUserDate = updateSaleDate;
            salesOption.UpdateUserNo = updateSaleNo;
            
        }

        //获取要更新列，通过赋值只更新改变的字段
        public static T UpdateChangedField<T>(this T bodyObject, T dbObject) where T : new()
        {
            T newObject = new T();
            Type t = newObject.GetType();

            //把order属性及其值用dictionary存储起来
            //Dictionary<string, object> dic = new Dictionary<string, object>();
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

        //判断DateRange是否为空
        public static bool JudgeDateRange(this DateRange dateRange)
        {
            if(dateRange != null && dateRange.DateMax != null && dateRange.DateMin != null)
            {
                return true;
            }

            return false;
        }

        //多种查询并分页
        public static List<Order> MultipleGet(this OrderContext orderContext, string propertyName, MultipleGetStyleOption getStyleOption, int pageSize, int pageNum)
        {
            var ordersQuery = orderContext.orders.AsNoTracking();
            if(getStyleOption != null)
            {
                //模糊查询
                if(getStyleOption.OrderNo != null)
                {
                    ordersQuery = ordersQuery.Where(o => o.No.Contains(getStyleOption.OrderNo));
                }

                if(getStyleOption.ClientName != null)
                {
                    ordersQuery = ordersQuery.Where(o => o.ClientName.Contains(getStyleOption.ClientName));
                }

                if(getStyleOption.Comment != null)
                {
                    ordersQuery = ordersQuery.Where(o => o.Comment.Contains(getStyleOption.Comment));
                }

                //状态查询
                List<Expression<Func<Order, bool>>> expressions = new List<Expression<Func<Order, bool>>>();               
                if (getStyleOption.State1 != null)
                {
                    Expression<Func<Order,bool>> expression1 = o => o.Status == (int)getStyleOption.State1;
                    expressions.Add(expression1);
                }
                if (getStyleOption.State2 != null)
                {
                    Expression<Func<Order, bool>> expression2 = o => o.Status == (int)getStyleOption.State2;
                    expressions.Add(expression2);
                }
                if(expressions.Count > 0)
                {
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

                    ordersQuery = ordersQuery.Where(lambda);
                }
                
                //日期范围查询
                if (getStyleOption.SignDateRange.JudgeDateRange())
                {
                    ordersQuery = ordersQuery.Where(o => o.SignDate >= getStyleOption.SignDateRange.DateMin && o.SignDate <= getStyleOption.SignDateRange.DateMax);
                }

                if (getStyleOption.CreateOrderDateRange.JudgeDateRange())
                {
                    ordersQuery = ordersQuery.Where(o => o.CreateUserDate >= getStyleOption.CreateOrderDateRange.DateMin && o.CreateUserDate <= getStyleOption.CreateOrderDateRange.DateMax);
                }

                if (getStyleOption.UpdateOrderDateRange.JudgeDateRange())
                {
                    ordersQuery = ordersQuery.Where(o => o.UpdaeUserDate >= getStyleOption.UpdateOrderDateRange.DateMin && o.UpdaeUserDate <= getStyleOption.UpdateOrderDateRange.DateMax);
                }

                //根据物料编号查询       
            }

            
            //排序，默认按照创建日期倒序排序
            if(propertyName == null)
            {
                ordersQuery = ordersQuery.OrderByDescending(o => o.CreateUserDate);
            }
            else
            {
                Type t = typeof(Order);
                PropertyInfo[] pi = t.GetProperties();
                PropertyInfo info = pi.Where(o => o.Name == propertyName).FirstOrDefault();

                //ordersQuery = ordersQuery.OrderBy(o => info.GetValue(o)).AsQueryable();

                ParameterExpression param = Expression.Parameter(typeof(Order), "order");
                MemberExpression s = Expression.Property(param, typeof(Order).GetProperty(propertyName, BindingFlags.IgnoreCase));
                var lambda = Expression.Lambda<Func<Order, object>>(s, param);
                ordersQuery = ordersQuery.OrderBy(lambda);
            }

            //分页
            ordersQuery = ordersQuery.Skip(pageSize * (pageNum - 1)).Take(pageSize);
            return ordersQuery.ToList();
        }
    }
}
