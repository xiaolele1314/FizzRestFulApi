using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fizz.SalesOrder.Interface
{
    public interface IUserService
    {
        public void setUser(string userName);
        public string getUser();
    }
}
