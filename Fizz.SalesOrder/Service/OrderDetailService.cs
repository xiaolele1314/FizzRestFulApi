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


        public ResultMessage<OrderDetail> CreatOrderDetails(string userName, OrderDetail detail, string orderNo)
        {
            //获取用户
            User user = CommonService.CreateUser(userName, null);

            //判断销售订单是否存在
            var f = user.OrderNos.Find(o => o == orderNo);

            if (f == null)
            {
                return new ResultMessage<OrderDetail> { Code = 203, Message = "该用户没有此销售订单", ResultObject = null };
            }

            //添加销售订单明细
            detail.OrderNo = orderNo;

            DateTime now = System.DateTime.Now;
            detail.SetCommonValue(now, userName, now, userName);

            _context.Add(detail);

            _context.SaveChanges();

            return new ResultMessage<OrderDetail> { Code = 200, Message = "OK", ResultObject = detail };
        }



        public ResultMessage<OrderDetail> DeleteDetailByKey(int detailNo)
        {
            var detail = _context.orderDetails.Find(detailNo);

            _context.orderDetails.Remove(detail);

            _context.SaveChanges();

            return new ResultMessage<OrderDetail> { Code = 200, Message = "OK", ResultObject = null };
        }

        public ResultMessage<OrderDetail> DeleteDetailByOrder(string orderNo)
        {
            var details = _context.orderDetails.Where(o => o.OrderNo == orderNo);
            foreach (OrderDetail item in details)
            {
                _context.orderDetails.Remove(item);
            }

            _context.SaveChanges();

            return new ResultMessage<OrderDetail> { Code = 200, Message = "OK", ResultObject = null };
        }

        public ResultMessage<OrderDetail> DeleteDetailByUser(string userName)
        {
            //获取用户
            User user = CommonService.CreateUser(userName, null);

            var details = _context.orderDetails.Where(o => user.OrderNos.Contains(o.OrderNo));
            foreach (OrderDetail item in details)
            {
                _context.orderDetails.Remove(item);
            }

            _context.SaveChanges();

            return new ResultMessage<OrderDetail> { Code = 200, Message = "OK", ResultObject = null };
        }


        public PageData<OrderDetail> QueryDetailByKey(int detailNo )
        {
            var orderPages = _context.orderDetails
                .AsNoTracking()
                .Where(o => o.RowNo == detailNo)
                .ToList();

            return new PageData<OrderDetail> { PageNo = 1, PageCount = 1, PageItems = orderPages };
        }

        public PageData<OrderDetail> QueryDetailByOrder(string orderNo, int pageSize, int pageNum)
        {
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

     
        public ResultMessage<OrderDetail> UpdateOrderDetail(string userName, string orderNo, int detailNo, OrderDetail detail)
        {
            //获取用户
            User user = CommonService.CreateUser(userName, null);


            if (detail.RowNo != 0)
            {
                return new ResultMessage<OrderDetail> { Code = 203, Message = "不能修改项目号", ResultObject = null };
            }

            //判断订单是否存在
            if (user.OrderNos.Find(o => o == orderNo) == null)
            {
                return new ResultMessage<OrderDetail> { Code = 204, Message = "订单不存在", ResultObject = null };
            }

            //判断明细是否存在
            var oldDetail = _context.orderDetails.Where(o => o.RowNo == detailNo).AsNoTracking().FirstOrDefault();
            if (oldDetail == null)
            {
                return new ResultMessage<OrderDetail> { Code = 205, Message = "订单明细不存在", ResultObject = null };
            }

            //修改明细数据
            DateTime now = System.DateTime.Now;
            detail.SetCommonValue(detail.CreateUserDate, detail.CreateUserNo, now, userName);
            
            detail.OrderNo = orderNo;
            detail.RowNo = detailNo;
            
            detail.UpdateChangedField<OrderDetail>(oldDetail);
            var u = _context.orderDetails.Update(detail);


            _context.SaveChanges();

            return new ResultMessage<OrderDetail> { Code = 200, Message = "OK", ResultObject = detail };
        }

       
    }
}
