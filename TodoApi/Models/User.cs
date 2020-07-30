using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace TodoApi.Models
{
    public class User
    {
        
        public User()
        {
            no = new List<string>();
        }
        public int id { get; set; }

        public  List<string> no { get; set; }
        public string name { get; set; }
        
    }
}
