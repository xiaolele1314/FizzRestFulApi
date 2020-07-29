using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
namespace TodoApi.Models
{
    public class OrderDetail
    {
        [DefaultValue("")]
        [Column(TypeName = ColumnTypes.NVarchar10)]
        [Required]
        public string No { get; set; }

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

        [DefaultValue("")]
        [Column(TypeName = ColumnTypes.NVarchar2000)]
        public string Comment { get; set; }

        [DefaultValue("")]
        [Column(TypeName = ColumnTypes.NVarchar100)]
        public string CreatNo { get; set; }

        [DefaultValue(typeof(DateTime), "0001-01-01")]
        public DateTime CreatDate { get; set; }

        [DefaultValue("")]
        [Column(TypeName = ColumnTypes.NVarchar100)]
        public string UpdateNo { get; set; }

        [DefaultValue(typeof(DateTime), "0001-01-01")]
        public DateTime UpdaeDate { get; set; }
    }
}
