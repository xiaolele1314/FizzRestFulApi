using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Models
{
    public class PageData
    {
        public PageData()
        {
            
        }
        public int pageNo { get; set; }
        public Object pageItems { get; set; }
      
    }
}
