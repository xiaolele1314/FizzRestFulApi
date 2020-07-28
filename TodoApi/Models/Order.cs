using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string No { get; set; }
        
        public string ClientName { get; set; }
        
        public DateTime SignDate { get; set; }
        
        public int Status { get; set; }
        
        public string Comment { get; set; }
        
        public string CreatNo { get; set; }
        
        public DateTime CreatDate { get; set; }
        
        public string UpdateNo { get; set; }
        
        public DateTime UpdaeDate { get; set; }
        
        public OrderDetail Detail { get; set; }
    }
}
