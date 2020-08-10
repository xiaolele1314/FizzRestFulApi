using Fizz.SalesOrder.Extensions;
using Fizz.SalesOrder.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fizz.SalesOrder.Extensions;

namespace Fizz.SalesOrder.Service
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly OrderDetailContext _context;

        public OrderDetailService(OrderDetailContext context)
        {
            this._context = context;

        }


        public ResultMes CreatOrderDetails(string userName, OrderDetail detail, string orderNo)
        {
            //获取用户
            User user = CommonService.CreateUser(userName, null);

            //判断销售订单是否存在
            var f = user.OrderNos.Find(o => o == orderNo);

            if (f == null)
            {
                return new ResultMes { Code = 203, Message = "该用户没有此销售订单" };
            }

            //添加销售订单明细
            detail.OrderNo = orderNo;

            DateTime now = System.DateTime.Now;
            detail.SetCommonValue(now, userName, now, userName);

            _context.Add(detail);

            _context.SaveChanges();

            return new ResultMes { Code = 200, Message = "OK" };
        }

        public ResultMes DeleteDetailAll()
        { 
            var details = _context.orderDetails.ToList();
            foreach (OrderDetail item in details)
            {
                _context.orderDetails.Remove(item);
            }

            _context.SaveChanges();

            return new ResultMes { Code = 200, Message = "OK" };
        }

        public ResultMes DeleteDetailByKey(string userName, int detailNo)
        {
            //获取用户
            User user = CommonService.CreateUser(userName, null);


            var detail = _context.orderDetails.Find(detailNo);

            _context.orderDetails.Remove(detail);

            _context.SaveChanges();

            return new ResultMes { Code = 200, Message = "OK" };
        }

        public ResultMes DeleteDetailByOrder(string userName, string orderNo)
        {
            //获取用户
            User user = CommonService.CreateUser(userName, null);

            var details = _context.orderDetails.Where(o => o.OrderNo == orderNo);
            foreach (OrderDetail item in details)
            {
                _context.orderDetails.Remove(item);
            }

            _context.SaveChanges();

            return new ResultMes { Code = 200, Message = "OK" };
        }

        public ResultMes DeleteDetailByUser(string userName)
        {
            //获取用户
            User user = CommonService.CreateUser(userName, null);

            var details = _context.orderDetails.Where(o => user.OrderNos.Contains(o.OrderNo));
            foreach (OrderDetail item in details)
            {
                _context.orderDetails.Remove(item);
            }

            _context.SaveChanges();

            return new ResultMes { Code = 200, Message = "OK" };
        }

        public PageData<OrderDetail> QueryDetailAll(int pageSize, int pageNum)
        {


            if (pageNum < 0)
            {
                throw new Exception("pageNum error!");
            }
            int count = _context.orderDetails.Count();
            decimal pageCount = Math.Ceiling((decimal)count / pageSize);

            if (pageNum > pageCount)
            {
                throw new Exception("pageNum too large!");
            }
            //分页查询

            var orderPages = _context.orderDetails
                .AsNoTracking()
                .Skip(pageSize * (pageNum - 1))
                .Take(pageSize)
                .ToList();

            return new PageData<OrderDetail> { PageNo = pageNum, PageCount = pageCount, PageItems = orderPages };
        }

        public PageData<OrderDetail> QueryDetailByKey(string userName, int detailNo )
        {
            //获取用户
            User user = CommonService.CreateUser(userName, null);

            var orderPages = _context.orderDetails
                .AsNoTracking()
                .Where(o => o.ProNo == detailNo)
                .ToList();

            return new PageData<OrderDetail> { PageNo = 1, PageCount = 1, PageItems = orderPages };
        }

        public PageData<OrderDetail> QueryDetailByOrder(string userName, string orderNo, int pageSize, int pageNum)
        {

            //获取用户
            User user = CommonService.CreateUser(userName, null);

            if (pageNum < 0)
            {
                throw new Exception("pageNum error!");
            }

            decimal pageCount = Math.Ceiling((decimal)_context.orderDetails.Where(o => o.OrderNo == orderNo).Count() / pageSize);

            if (pageNum > pageCount)
            {
                throw new Exception("pageNum too large!");
            }
            //分页查询

            var orderPages = _context.orderDetails.AsNoTracking()
                .Where(o => o.OrderNo == orderNo)
                .Skip(pageSize * (pageNum - 1))
                .Take(pageSize)
                .ToList();

            return new PageData<OrderDetail> { PageNo = pageNum, PageCount = pageCount, PageItems = orderPages };
        }


        public PageData<OrderDetail> QueryDetailByUser(string userName, int pageSize, int pageNum)
        {
            // _context.Database.EnsureCreated();

            //获取用户
            User user = CommonService.CreateUser(userName, null);

            if(pageNum < 0)
            {
                throw new Exception("pageNum error!");
            }

            decimal pageCount = Math.Ceiling((decimal)_context.orderDetails.Where(o => user.OrderNos.Contains(o.OrderNo)).Count() / pageSize);

            if(pageNum > pageCount)
            {
                throw new Exception("pageNum too large!");
            }
            //分页查询
           
            var orderPages = _context.orderDetails.AsNoTracking()
                .Where(o => user.OrderNos.Contains(o.OrderNo))
                .Skip(pageSize * (pageNum - 1))
                .Take(pageSize)
                .ToList();

            return new PageData<OrderDetail> { PageNo = pageNum, PageCount = pageCount, PageItems = orderPages };
        }

     
        public ResultMes UpdateOrderDetail(string userName, string orderNo, int detailNo, OrderDetail detail)
        {
            //获取用户
            User user = CommonService.CreateUser(userName, null);


            if (detail.ProNo != 0)
            {
                return new ResultMes { Code = 203, Message = "不能修改项目号" };
            }

            //判断订单是否存在
            if (user.OrderNos.Find(o => o == orderNo) == null)
            {
                return new ResultMes { Code = 204, Message = "订单不存在" };
            }

            //判断明细是否存在
            var oldDetail = _context.orderDetails.Where(o => o.ProNo == detailNo).AsNoTracking().FirstOrDefault();
            if (oldDetail == null)
            {
                return new ResultMes { Code = 205, Message = "订单明细不存在" };
            }

            //修改明细数据
            DateTime now = System.DateTime.Now;
            detail.SetCommonValue(detail.CreateUserDate, detail.CreateUserNo, now, userName);
            
            detail.OrderNo = orderNo;
            detail.ProNo = detailNo;
            
            detail.UpdateChangedField<OrderDetail>(oldDetail);
            var u = _context.orderDetails.Update(detail);


            _context.SaveChanges();

            return new ResultMes { Code = 200, Message = "OK" };
        }

       
    }
}
