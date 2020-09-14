using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fizz.SalesOrder.Models
{
    public class SalesCommonBase
    {
        [Column(TypeName = ColumnTypes.NVarchar100)]
        public string CreateUserNo { get; set; }

        public DateTime CreateUserDate { get; set; }

        [Column(TypeName = ColumnTypes.NVarchar100)]
        public string UpdateUserNo { get; set; }
        public DateTime UpdateUserDate { get; set; }
    }
}
