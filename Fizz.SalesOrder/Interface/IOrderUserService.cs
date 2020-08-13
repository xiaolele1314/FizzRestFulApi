using Fizz.SalesOrder.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fizz.SalesOrder.Interface
{
    public interface IOrderUserService
    {
        IActionResult QureyOrderByUser(int? pageSize, int? pageNum);
        IActionResult QueryDetailByUser(int? pageSize, int? pageNum);
        IActionResult DeleteDetailByUser();
    }
}
