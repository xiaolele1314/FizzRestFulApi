using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fizz.SalesOrder.Models
{
    public class OrderDto:SalesCommonBase
    {
        public string No { get; set; }

        public string ClientName { get; set; }

        public DateTime SignDate { get; set; }

        public int? Status { get; set; }

        public string Comment { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
    }
}
