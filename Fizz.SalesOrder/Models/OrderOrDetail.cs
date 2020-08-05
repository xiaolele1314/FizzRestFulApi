using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fizz.SalesOrder.Models
{
    public class OrderOrDetail
    {
        public Order Order { get; set; }
        public OrderDetail Detail { get; set; }
    }
}
