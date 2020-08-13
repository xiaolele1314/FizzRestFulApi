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
    }
}
