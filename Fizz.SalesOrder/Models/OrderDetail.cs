using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
namespace Fizz.SalesOrder.Models
{
    [Table("detail")]
    public class OrderDetail:SalesCommonBase
    {
        [DefaultValue("")]
        [Required]
        [ForeignKey("No")]
        [Key, Column(Order = 1, TypeName = ColumnTypes.NVarchar10)]
        public string OrderNo { get; set; }

        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DefaultValue(0)]
        [Required]
        public int RowNo { get; set; }
       
        [DefaultValue("")]
        [Column(TypeName = ColumnTypes.NVarchar10)]
        [Required]
        public string MaterialNo { get; set; }
       
        [DefaultValue(0)]
        [Required]
        public double? Amount { get; set; }
       
        [DefaultValue("")]
        [Column(TypeName = ColumnTypes.NVarchar10)]
        [Required]
        public string Unit { get; set; }
       
        [DefaultValue(0)]
        public int? SortNo { get; set; }

        [DefaultValue("")]
        [Column(TypeName = ColumnTypes.NVarchar2000)]
        public string Comment { get; set; }

        [NotMapped]
        public Order Order { get; set; }
    }
}
