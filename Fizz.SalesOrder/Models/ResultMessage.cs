using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fizz.SalesOrder.Models
{
    public class ResultMessage<T>
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public T ResultObject { get; set; }
    }
}
