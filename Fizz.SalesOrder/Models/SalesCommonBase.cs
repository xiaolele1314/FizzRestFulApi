using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Fizz.SalesOrder.Models
{
    public class SalesCommonBase
    {
        [DefaultValue("")]
        [Column(TypeName = ColumnTypes.NVarchar100)]
        public string CreateUserNo { get; set; }

        [DefaultValue(typeof(DateTime), "0001-01-01")]
        public DateTime CreateUserDate { get; set; }

        [DefaultValue("")]
        [Column(TypeName = ColumnTypes.NVarchar100)]
        public string UpdateUserNo { get; set; }

        [DefaultValue(typeof(DateTime), "0001-01-01")]
        public DateTime UpdateUserDate { get; set; }
    }
}
