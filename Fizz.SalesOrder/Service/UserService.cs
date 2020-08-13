using Fizz.SalesOrder.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fizz.SalesOrder.Service
{
    public class UserService: IUserService
    {
        private string UserName { get; set; }

        public string getUser()
        {
            return this.UserName;
        }

        public void setUser(string userName)
        {
            this.UserName = userName;
        }
    }
}
