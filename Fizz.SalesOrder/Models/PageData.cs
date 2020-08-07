using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fizz.SalesOrder.Models
{
    public class PageData<T>
    {
        public decimal PageCount { get; set; }
        public int PageNo { get; set; }
        public List<T> PageItems { get; set; }
      
    }
}
