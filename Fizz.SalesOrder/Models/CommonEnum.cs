using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fizz.SalesOrder.Models
{
    public enum GetTypeEnum { GetAll = 1, GetByKey = 2, GetByUser = 3, GetByOrder = 4 };
    public enum DeleteTypeEnum { DeleteAll = 1, DeleteByKey = 2, DeleteByUser = 3, DeleteByOrder = 4 };
    public enum OrderStatusEnum { Pending = 0, Dispose = 1, Cancel = 2, Finish = 3 };
}
