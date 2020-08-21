using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fizz.SalesOrder.Models
{
    public class OrderDetailDto:SalesCommonBase
    {
        public string OrderNo { get; set; }

        public int RowNo { get; set; }

        public string MaterialNo { get; set; }

        public double? Amount { get; set; }

        public string Unit { get; set; }

        public int? SortNo { get; set; }

        public string Comment { get; set; }

        public Order Order { get; set; }
    }
}
