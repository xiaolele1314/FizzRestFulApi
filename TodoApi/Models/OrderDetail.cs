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
       
        public string No { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProNo { get; set; }
       
        public string MaterialNo { get; set; }
       
        public double Amount { get; set; }
       
        public string Unit { get; set; }
       
        public int SortNo { get; set; }
       
        public string Comment { get; set; }
        
        public string CreatNo { get; set; }
        
        public DateTime CreatDate { get; set; }
        
        public string UpdateNo { get; set; }
       
        public DateTime UpdaeDate { get; set; }
    }
}
