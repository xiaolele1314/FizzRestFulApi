using AutoMapper;
using Fizz.SalesOrder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fizz.SalesOrder.Controllers
{
    public class AutoMapperConfigs:Profile
    {
        public AutoMapperConfigs()
        {
            CreateMap<OrderDto, Order>()
                .ForMember(des => des.No, opt => opt.Ignore())
                .ForMember(des => des.ClientName, opt => opt.Condition(src => src.ClientName != null))
                .ForMember(des => des.Comment, opt => opt.Condition(src => src.Comment != null))
                .ForMember(des => des.CreateUserDate, opt => opt.Condition(src => src.CreateUserDate != DateTime.MinValue))
                .ForMember(des => des.CreateUserNo, opt => opt.Condition(src => src.CreateUserNo != null))
                .ForMember(des => des.SignDate, opt => opt.Condition(src => src.SignDate != DateTime.MinValue))
                .ForMember(des => des.Status, opt => opt.Condition(src => src.Status != null))
                .ForMember(des => des.OrderDetails, opt => opt.Condition(src => src.OrderDetails != null));

            CreateMap<OrderDetailDto, OrderDetail>()
                .ForMember(des => des.RowNo, opt => opt.Ignore())
                .ForMember(des => des.OrderNo, opt => opt.Ignore())
                .ForMember(des => des.Comment, opt => opt.Condition(src => src.Comment != null))
                .ForMember(des => des.CreateUserDate, opt => opt.Condition(src => src.CreateUserDate != DateTime.MinValue))
                .ForMember(des => des.CreateUserNo, opt => opt.Condition(src => src.CreateUserNo != null))
                .ForMember(des => des.MaterialNo, opt => opt.Condition(src => src.MaterialNo != null))
                .ForMember(des => des.SortNo, opt => opt.Condition(src => src.SortNo != null))
                .ForMember(des => des.Order, opt => opt.Condition(src => src.Order != null))
                .ForMember(des => des.Unit, opt => opt.Condition(src => src.Unit != null))
                .ForMember(des => des.Amount, opt => opt.Condition(src => src.Amount != null));
                

        }
    }
}
