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
        [Column(TypeName = ColumnTypes.NVarchar10)]
        [Required]
        [ForeignKey("No")]
        public string OrderNo { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DefaultValue(0)]
        [Required]
        public int ProNo { get; set; }
       
        [DefaultValue("")]
        [Column(TypeName = ColumnTypes.NVarchar10)]
        [Required]
        public string MaterialNo { get; set; }
       
        [DefaultValue(0)]
        [Required]
        public double Amount { get; set; }
       
        [DefaultValue("")]
        [Column(TypeName = ColumnTypes.NVarchar10)]
        [Required]
        public string Unit { get; set; }
       
        [DefaultValue(0)]
        public int SortNo { get; set; }

        public static explicit operator OrderDetail(Order v)
        {
            throw new NotImplementedException();
        }

        [DefaultValue("")]
        [Column(TypeName = ColumnTypes.NVarchar2000)]
        public string Comment { get; set; }

        public Order Order { get; set; }
    }
}
