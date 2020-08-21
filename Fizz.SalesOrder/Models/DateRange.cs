using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fizz.SalesOrder.Models
{
    public class DateRange
    {
        public DateTime? DateMin { get; set; } = DateTime.MinValue;

        public DateTime? DateMax { get; set; } = DateTime.MaxValue;
    }
}
