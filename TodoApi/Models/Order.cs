using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Models
{

    public enum State { Pending = 0 , Dispose = 1, Cancel = 2, Finish = 3 };

    public class Order
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
        
        [DefaultValue((int)State.Pending)]
        public int Status { get; set; }

        [DefaultValue("")]
        [Column(TypeName = ColumnTypes.NVarchar2000)]
        public string Comment { get; set; }

        [DefaultValue("")]
        [Column(TypeName = ColumnTypes.NVarchar100)]
        public string CreatNo { get; set; }

        [DefaultValue(typeof(DateTime),"0001-01-01")]
        public DateTime CreatDate { get; set; }

        [DefaultValue("")]
        [Column(TypeName = ColumnTypes.NVarchar100)]
        public string UpdateNo { get; set; }

        [DefaultValue(typeof(DateTime), "0001-01-01")]
        public DateTime UpdaeDate { get; set; }


        public List<OrderDetail> orderDetails { get; set; }
        
        //public OrderDetail Detail { get; set; }
    }
}
