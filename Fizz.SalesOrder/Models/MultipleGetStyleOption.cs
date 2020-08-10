using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fizz.SalesOrder.Models
{
    public class MultipleGetStyleOption
    {
        public string OrderNo { get; set; }
        public string ClientName { get; set; }
        public string Comment { get; set; }
        //public List<OrderStatusEnum> OrderStatus { get; set; }
        public OrderStatusEnum? State1 { get; set; }
        public OrderStatusEnum? State2 { get; set; }

        public DateRange SignDateRange { get; set; }
        public DateRange CreateOrderDateRange { get; set; }
        public DateRange UpdateOrderDateRange { get; set; }
        //public List<string> DetailMaterialNos { get; set; }
    }
}
