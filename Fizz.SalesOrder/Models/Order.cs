using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Fizz.SalesOrder.Models
{

    [Table("order")]
    public class Order:SalesCommonBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DefaultValue("")]
        [Column(TypeName = ColumnTypes.NVarchar10)]
        public string No { get; set; }
        
        [Required]
        [DefaultValue("")]
        [Column(TypeName = ColumnTypes.NVarchar10)]
        public string ClientName { get; set; }
        
        [Required]
        [DefaultValue(typeof(DateTime), "0001-01-01")]
        public DateTime SignDate { get; set; }
        
        [DefaultValue((int)OrderStatusEnum.Pending)]
        public int Status { get; set; }

        [DefaultValue("")]
        [Column(TypeName = ColumnTypes.NVarchar2000)]
        public string Comment { get; set; }



        public List<OrderDetail> OrderDetails { get; set; }
        
        //public OrderDetail Detail { get; set; }
    }
}
