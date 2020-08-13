using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fizz.SalesOrder.Models
{
    public class MultipleGetStyleOption
    {
        private int? pageSize;
        public int? PageSize
        {
            get
            {
                if (this.pageSize == null)
                {
                    this.pageSize = 100;
                }

                return this.pageSize;
            }
            set
            {
                this.pageSize = value;
            }
        }

        private int? pageNum;
        public int? PageNum
        {
            get
            {
                if(this.pageNum == null)
                {
                    this.pageNum = 1;
                }
                return this.pageNum;
            }
            set
            {
                this.pageNum = value;
            }
        }

        private string sortStr;
        public string SortStr
        {
            get
            {
                if (this.sortStr == null)
                {
                    this.sortStr = "CreateUserDate";
                }
                return this.sortStr;
            }
            set
            {
                this.sortStr = value;
            }
        }
        public string OrderNo { get; set; }
        public string ClientName { get; set; }
        public string Comment { get; set; }

        private OrderStatusEnum[] status;
        public OrderStatusEnum[] Status
        {
            get
            {
                if(this.status == null)
                {
                    this.status = new OrderStatusEnum[] { OrderStatusEnum.Pending, OrderStatusEnum.Dispose, OrderStatusEnum.Cancel, OrderStatusEnum.Finish };
                }
                return this.status;
            }
            set
            {
                this.status = value;
            }
        }

        private DateRange signDateRange;
        public DateRange SignDateRange
        {
            get
            {
                if (this.signDateRange == null)
                {
                    this.signDateRange = new DateRange();
                }
                return this.signDateRange;
            }
            set
            {
                this.signDateRange = value;
            }
        }

        private DateRange createOrderDateRange;
        public DateRange CreateOrderDateRange
        {
            get
            {
                if (this.createOrderDateRange == null)
                {
                    this.createOrderDateRange = new DateRange();
                }
                return this.createOrderDateRange;
            }
            set
            {
                this.createOrderDateRange = value;
            }
        }

        private DateRange updateOrderDateRange;
        public DateRange UpdateOrderDateRange
        {
            get
            {
                if (this.updateOrderDateRange == null)
                {
                    this.updateOrderDateRange = new DateRange();
                }
                return this.updateOrderDateRange;
            }
            set
            {
                this.updateOrderDateRange = value;
            }
        }
        
    }
}
