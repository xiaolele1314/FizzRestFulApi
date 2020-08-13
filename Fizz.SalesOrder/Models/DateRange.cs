using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fizz.SalesOrder.Models
{
    public class DateRange
    {
        private DateTime? dateMin;
        public DateTime? DateMin
        {
            get
            {
                if (this.dateMin == null)
                {
                    this.dateMin = DateTime.MinValue;
                }
                return this.dateMin;
            }
            set
            {
                this.dateMin = value;
            }
        }

        private DateTime? dateMax;
        public DateTime? DateMax
        {
            get
            {
                if (this.dateMax == null)
                {
                    this.dateMax = DateTime.MaxValue;
                }
                return this.dateMax;
            }
            set
            {
                this.dateMax = value;
            }
        }
    }
}
