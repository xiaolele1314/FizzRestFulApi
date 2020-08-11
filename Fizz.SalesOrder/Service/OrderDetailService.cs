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
        private readonly SalesContext _context;

        public OrderDetailService(SalesContext context)
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

            if(_context.details.AsNoTracking().Where(o => o.RowNo == detail.RowNo && o.OrderNo == orderNo).FirstOrDefault() != null)
            {
                return new ResultMessage<OrderDetail> { Code = 203, Message = "该订单明细已存在", ResultObject = null };
            }
            //添加销售订单明细
            detail.OrderNo = orderNo;

            DateTime now = System.DateTime.Now;
            detail.SetCommonValue(now, userName, now, userName);

            _context.Add(detail);

            _context.SaveChanges();

            return new ResultMessage<OrderDetail> { Code = 200, Message = "OK", ResultObject = detail };
        }



        public ResultMessage<OrderDetail> DeleteDetailByKey(string orderNo, int detailNo)
        {
            var detail = _context.details.Find(new object[] { detailNo, orderNo});

            if(detail == null)
            {
                return new ResultMessage<OrderDetail> { Code = 400, Message = "订单或明细不存在", ResultObject = null };
            }
            _context.details.Remove(detail);

            _context.SaveChanges();

            return new ResultMessage<OrderDetail> { Code = 200, Message = "OK", ResultObject = null };
        }

        public ResultMessage<OrderDetail> DeleteDetailByOrder(string orderNo)
        {
            var details = _context.details.Where(o => o.OrderNo == orderNo);

            if (details.Count() == 0)
            {
                return new ResultMessage<OrderDetail> { Code = 400, Message = "订单或明细不存在", ResultObject = null };
            }

            foreach (var item in details)
            {
                _context.details.Remove(item);
            }

            _context.SaveChanges();

            return new ResultMessage<OrderDetail> { Code = 200, Message = "OK", ResultObject = null };
        }

        public object QueryDetailByKey(string orderNo, int detailNo )
        {
            var order = _context.details
                .AsNoTracking()
                .Where(o => o.RowNo == detailNo && o.OrderNo == orderNo)
                .FirstOrDefault();

            if(order == null)
            {
                return new ResultMessage<OrderDetail> { Code = 400, Message = "订单编号或明细不存在", ResultObject = null };
            }
            return order;
        }

        public object QueryDetailByOrder(string orderNo, int? pageSize, int? pageNum)
        {
            var details = _context.details.AsNoTracking().Where(o => o.OrderNo == orderNo);

            if(details.Count() == 0)
            {
                return new ResultMessage<OrderDetail> { Code = 400, Message = "订单编号或明细不存在", ResultObject = null };
            }
           

            decimal? pageCount = Math.Ceiling((decimal)((decimal)details.Count() / pageSize));

            if (pageNum > pageCount || pageNum <= 0)
            {
                return new ResultMessage<OrderDetail> { Code = 400, Message = "pageNum错误", ResultObject = null };
            }
            //分页查询

            var orderPages = details
                .Skip((int)(pageSize * (pageNum - 1)))
                .Take((int)pageSize)
                .ToList();

            if (orderPages == null)
            {
                return new ResultMessage<OrderDetail> { Code = 400, Message = "订单编号或明细不存在", ResultObject = null };
            }

            return new PageData<OrderDetail> { PageNum = pageNum, PageCount = (int)pageCount, PageItems = orderPages };
        }
     
        public ResultMessage<OrderDetail> UpdateOrderDetail(string userName, string orderNo, int detailNo, OrderDetail detail)
        {
            //获取用户
            User user = CommonService.CreateUser(userName, null);


            if (detail.RowNo != 0)
            {
                return new ResultMessage<OrderDetail> { Code = 203, Message = "不能修改项目号", ResultObject = null };
            }

            //判断明细是否存在
            var oldDetail = _context.details.Where(o => o.RowNo == detailNo && o.OrderNo == orderNo).AsNoTracking().FirstOrDefault();
            if (oldDetail == null)
            {
                return new ResultMessage<OrderDetail> { Code = 400, Message = "订单或明细不存在", ResultObject = null };
            }

            //修改明细数据
            DateTime now = System.DateTime.Now;
            detail.SetCommonValue(detail.CreateUserDate, detail.CreateUserNo, now, userName);
            
            detail.OrderNo = orderNo;
            detail.RowNo = detailNo;
            
            detail.UpdateChangedField<OrderDetail>(oldDetail);

            var u = _context.details.Update(detail);
            _context.SaveChanges();

            return new ResultMessage<OrderDetail> { Code = 200, Message = "OK", ResultObject = detail };
        }

       
    }
}
