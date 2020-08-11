using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fizz.SalesOrder.Models
{
    public class MultipleGetStyleOption
    {
        public int? PageSize { get; set; }
        public int? PageNum { get; set; }
        public string SortName { get; set; }
        public string OrderNo { get; set; }
        public string ClientName { get; set; }
        public string Comment { get; set; }
        public OrderStatusEnum[] Status { get; set; }
        public DateRange SignDateRange { get; set; }
        public DateRange CreateOrderDateRange { get; set; }
        public DateRange UpdateOrderDateRange { get; set; }
        //public List<string> DetailMaterialNos { get; set; }
    }
}
