using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fizz.SalesOrder.Models
{
    public class MultipleGetStyleOption
    {

        public int? PageSize { get; set; } = 100;

        public int? PageNum { get; set; } = 1;

        public string SortStr { get; set; } = "CreateUserDate";

        public string OrderNo { get; set; }

        public string ClientName { get; set; }

        public string Comment { get; set; }

        public OrderStatusEnum[] Status { get; set; } = new OrderStatusEnum[] { OrderStatusEnum.Pending, OrderStatusEnum.Dispose, OrderStatusEnum.Cancel, OrderStatusEnum.Finish };

        public DateRange SignDateRange { get; set; } = new DateRange();

        public DateRange CreateOrderDateRange { get; set; } = new DateRange();
  
        public DateRange UpdateOrderDateRange { get; set; } = new DateRange();
        
    }
}
