using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Service
{
    public interface IOrderService
    {
        Order CreatOrder();
        void DeleteOrder();
        List<Order> QueryOrder();
        void UpdateOrder();

    }
}
