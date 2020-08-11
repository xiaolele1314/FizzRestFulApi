using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Fizz.SalesOrder.Models
{
    public class User
    {
        
        public User()
        {
            OrderNos = new List<string>();
        }

        public  List<string> OrderNos { get; set; }
        public string Name { get; set; }
        

    }
}
