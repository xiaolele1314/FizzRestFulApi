using Fizz.SalesOrder.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Fizz.SalesOrder.Extensions
{
    public static class FizzSaleExtensions
    {
        public static void SetCommonValue(this SalesCommonBase salesOption, DateTime createSaleDate, string createSaleNo, DateTime updateSaleDate, string updateSaleNo)
        {
            salesOption.CreatSaleDate = createSaleDate;
            salesOption.CreatSaleNo = createSaleNo;
            salesOption.UpdaeSaleDate = updateSaleDate;
            salesOption.UpdateSaleNo = updateSaleNo;
            
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
    }
}
