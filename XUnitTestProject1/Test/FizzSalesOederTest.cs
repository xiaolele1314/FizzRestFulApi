using Xunit;
using Test;
using Fizz.SalesOrder.Controllers;
using Fizz.SalesOrder.Models;
using Fizz.SalesOrder.Service;
using Fizz.SalesOrder.Interface;
using Microsoft.AspNetCore.Mvc;


namespace XUnitTestProject1
{
 
    [Collection("CollectionFixture")]
    public class FizzSalesOederTest:TestBase
    {       
        
        //controller
        private readonly OrderController _orderController;

        private readonly OrderDetailController _orderDetailController;

        private readonly OrderUserController _orderUserController;

        //service
        private readonly OrderService _orderService;

        private readonly OrderDetailService _orderDetailService;

        private readonly OrderUserService _orderUserService;


        

        public FizzSalesOederTest(ModuleFixture fixture)
            :base(fixture)
        {
            //this._orderController = new OrderController(new MockOrderService());

            //this._orderDetailController = new OrderDetailController(new MockOrderDetailService());

            //this._orderUserController = new OrderUserController(new MockOrderUserService());

            this._orderController = this.GetRequiredService<OrderController>();

            this._orderDetailController = this.GetRequiredService<OrderDetailController>();

            this._orderUserController = this.GetRequiredService<OrderUserController>();


            this._orderService = (OrderService)this.GetRequiredService<IOrderService>();

            this._orderDetailService = (OrderDetailService)this.GetRequiredService<IOrderDetailService>();

            this._orderUserService = (OrderUserService)this.GetRequiredService<IOrderUserService>();

           

        }


     
        //OrderController
        [Fact(DisplayName = "OrderController: 获取单个销售订单")]
        public void GetOrderTest()
        {
            this.MockEmptyEntity<Order>(o =>
            {
                o.No = "1";
                o.ClientName = "lele";
                o.Status = (int)OrderStatusEnum.Pending;
            });

            var result = (JsonResult)this._orderController.Get("1");
            Assert.Equal(200, result.StatusCode);
        }

        [Fact(DisplayName = "OrderController: 获取所有销售订单")]
        public void GetAllOrderTest()
        {
            this.MockEntities<Order>((o, i) =>
            {
                o.No = "1" + i;
                o.ClientName = "lele";
                o.Status = (int)OrderStatusEnum.Pending;
            }, 100);

            var result = (JsonResult)this._orderController.Get(new MultipleGetStyleOption());
            Assert.Equal(200, result.StatusCode);
        }

        [Fact(DisplayName = "OrderController: 删除单个销售订单")]
        public void DeleteOrderTest()
        {
            this.MockEmptyEntity<Order>(o =>
            {
                o.No = "1";
                o.ClientName = "lele";
                o.Status = (int)OrderStatusEnum.Pending;
            });

            var result = (JsonResult)this._orderController.Delete("1");
            Assert.Equal(200, result.StatusCode);
        }

        [Fact(DisplayName = "OrderController: 更改单个销售订单")]
        public void UpdateOrderTest()
        {
            this.MockEmptyEntity<Order>(o =>
            {
                o.No = "1";
                o.ClientName = "lele";
                o.Status = (int)OrderStatusEnum.Pending;
            });

            var orderDto = new OrderDto();
            orderDto.ClientName = "update";
            var result = (JsonResult)this._orderController.Put("1", orderDto);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact(DisplayName = "OrderController: 增加销售订单")]
        public void AddOrderTest()
        {
            var order = new Order();
            order.No = "1";
            order.ClientName = "lele";

            var result = (JsonResult)this._orderController.Post(order);
            Assert.Equal(200, result.StatusCode);
        }


        //OrderDetailController
        [Fact(DisplayName = "OrderDetailController: 获取单个明细单")]
        public void GetOrderDetailTest()
        {
            this.MockEmptyEntity<Order>(o =>
            {
                o.No = "1";
                o.ClientName = "lele";
                o.Status = (int)OrderStatusEnum.Pending;
            });

            this.MockEmptyEntity<OrderDetail>(o =>
            {
                o.OrderNo = "1";
                o.RowNo = 1;
                o.Amount = 500;
                o.MaterialNo = "100";
                o.Unit = "10";
            });

            var result = (JsonResult)this._orderDetailController.Get("1",1);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact(DisplayName = "OrderDetailController: 获取销售单下的所有明细单")]
        public void GetAllOrderDetailTest()
        {
            this.MockEmptyEntity<Order>(o =>
            {
                o.No = "1";
                o.ClientName = "lele";
                o.Status = (int)OrderStatusEnum.Pending;
            });

            this.MockEmptyEntity<OrderDetail>(o =>
            {
                o.OrderNo = "1";
                o.RowNo = 1;
                o.Amount = 500;
                o.MaterialNo = "100";
                o.Unit = "10";
            });

            var result = (JsonResult)this._orderDetailController.Get("1", null, null);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact(DisplayName = "OrderDetailController: 删除一个明细单")]
        public void DeleteOrderDetailTest()
        {
            this.MockEmptyEntity<Order>(o =>
            {
                o.No = "1";
                o.ClientName = "lele";
                o.Status = (int)OrderStatusEnum.Pending;
            });

            this.MockEmptyEntity<OrderDetail>(o =>
            {
                o.OrderNo = "1";
                o.RowNo = 1;
                o.Amount = 500;
                o.MaterialNo = "100";
                o.Unit = "10";
            });

            var result = (JsonResult)this._orderDetailController.Delete("1", 1);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact(DisplayName = "OrderDetailController: 删除销售单下的所有明细单")]
        public void DeleteOrderDetailOfOrderTest()
        {
            this.MockEmptyEntity<Order>(o =>
            {
                o.No = "1";
                o.ClientName = "lele";
                o.Status = (int)OrderStatusEnum.Pending;
            });

            this.MockEntities<OrderDetail>((o, i) =>
            {
                o.OrderNo = "1";
                o.RowNo = i;
                o.Amount = 500;
                o.MaterialNo = "100";
                o.Unit = "10";
            }, 100);

            var result = (JsonResult)this._orderDetailController.Delete("1");
            
            Assert.Equal(200, result.StatusCode);
        }

        [Fact(DisplayName = "OrderDetailController: 更改明细单")]
        public void UpdateOrderDetailTest()
        {
            this.MockEmptyEntity<Order>(o =>
            {
                o.No = "1";
                o.ClientName = "lele";
                o.Status = (int)OrderStatusEnum.Pending;
            });

            this.MockEmptyEntity<OrderDetail>(o =>
            {
                o.OrderNo = "1";
                o.RowNo = 1;
                o.Amount = 500;
                o.MaterialNo = "100";
                o.Unit = "10";
            });

            var detailDto = new OrderDetailDto();
            detailDto.Comment = "update";
            var result = (JsonResult)this._orderDetailController.Put("1", 1, detailDto);
            
            Assert.Equal(200, result.StatusCode);
        }

        [Fact(DisplayName = "OrderDetailController: 增加明细单")]
        public void AddOrderDetailTest()
        {
            this.MockEmptyEntity<Order>(o =>
            {
                o.No = "1";
                o.ClientName = "lele";
                o.Status = (int)OrderStatusEnum.Pending;
            });

          
            var detail = new OrderDetail();

            detail.RowNo = 1;
            detail.Amount = 500;
            detail.MaterialNo = "100";
            detail.Unit = "10";
            var result = (JsonResult)this._orderDetailController.Post(detail, "1");
            
            Assert.Equal(200, result.StatusCode);
        }

        //OrderUserController
        [Fact(DisplayName = "OrderUserController: 删除用户下的所有明细单")]
        public void DeleteUserAllOrderDetailTest()
        {
            this.MockEmptyEntity<Order>(o =>
            {
                o.No = "1";
                o.ClientName = "lele";
                o.Status = (int)OrderStatusEnum.Pending;
            });

            this.MockEntities<OrderDetail>((o, i) =>
            {
                o.OrderNo = "1";
                o.RowNo = i;
                o.Amount = 500;
                o.MaterialNo = "100";
                o.Unit = "10";
            }, 100);

            var result = (JsonResult)this._orderUserController.DeleteOrderDetail();
            
            Assert.Equal(200, result.StatusCode);
        }

        [Fact(DisplayName = "OrderUserController: 获取用户下的所有明细单")]
        public void GetUserAllOrderDetailTest()
        {
            this.MockEmptyEntity<Order>(o =>
            {
                o.No = "1";
                o.ClientName = "lele";
                o.Status = (int)OrderStatusEnum.Pending;
            });

            this.MockEntities<OrderDetail>((o,i) =>
            {
                o.OrderNo = "1";
                o.RowNo = i;
                o.Amount = 500;
                o.MaterialNo = "100";
                o.Unit = "10";
            },100);

            var result = (JsonResult)this._orderUserController.GetOrderDetail(default, default);
            
            Assert.Equal(200, result.StatusCode);
        }

        [Fact(DisplayName = "OrderUserController: 获取用户下的所有销售单")]
        public void GetUserAllOrderTest()
        {
            this.MockEntities<Order>((o,i) =>
            {
                o.No = "1"+i;
                o.ClientName = "lele";
                o.Status = (int)OrderStatusEnum.Pending;
            }, 100);

          
            var result = (JsonResult)this._orderUserController.GetOrder(default, default);
            
            Assert.Equal(200, result.StatusCode);
        }


        //OrderService
        [Fact(DisplayName = "OrderService: 创建销售单")]
        public void CreateOrderServiceTest()
        {
            var order = new Order();
            order.No = "1";
            order.ClientName = "lele";
            var result = (JsonResult)this._orderService.CreatOrder(order);

          
            
            Assert.Equal(200, result.StatusCode);
            
        }

        [Fact(DisplayName = "OrderService: 查询一个销售单")]
        public void GetOrderServiceTest()
        {
            this.MockEmptyEntity<Order>(o =>
            {
                o.No = "1";
                o.ClientName = "lele";
                o.Status = (int)OrderStatusEnum.Pending;
            });

            var result = (JsonResult)this._orderService.QueryOrder("1");
            Assert.Equal(200, result.StatusCode);
        }

        [Fact(DisplayName = "OrderService: 查询所有销售单")]
        public void GetAllOrderServiceTest()
        {
            this.MockEntities<Order>((o,i) =>
            {
                o.No = "1"+i;
                o.ClientName = "lele";
                o.Status = (int)OrderStatusEnum.Pending;
            },100);
            var result = (JsonResult)this._orderService.QueryOrder(new MultipleGetStyleOption());
            Assert.Equal(200, result.StatusCode);
        }

        [Fact(DisplayName = "OrderService: 删除一个销售单")]
        public void DeleteOrderServiceTest()
        {
            this.MockEmptyEntity<Order>(o =>
              {
                  o.No = "1";
                  o.ClientName = "lele";
                  o.Status = (int)OrderStatusEnum.Pending;
              });

            var result = (JsonResult)this._orderService.DeleteOrder("1");
            Assert.Equal(200, result.StatusCode);
        }

        [Fact(DisplayName = "OrderService: 更新一个销售单")]
        public void UpdateOrderServiceTest()
        {
            this.MockEmptyEntity<Order>(o =>
            {
                o.No = "1";
                o.ClientName = "lele";
                o.Status = (int)OrderStatusEnum.Pending;
            });

            var orderDto = new OrderDto();
            orderDto.ClientName = "update";
            var result = (JsonResult)this._orderService.UpdateOrder("1", orderDto);
            Assert.Equal(200, result.StatusCode);
        }



        //OrderDetailService
        [Fact(DisplayName = "OrderDetailService: 创建明细单")]
        public void CreateOrderDetailServiceTest()
        {
            this.MockEmptyEntity<Order>(o =>
            {
                o.No = "1";
                o.ClientName = "lele";
                o.Status = (int)OrderStatusEnum.Pending;
            });

            var detail = new OrderDetail();
            
            detail.RowNo = 1;
            detail.Amount = 500;
            detail.MaterialNo = "100";
            detail.Unit = "10";
            var result = (JsonResult)this._orderDetailService.CreateDetail(detail, "1");


            Assert.Equal(200, result.StatusCode);


        }

        [Fact(DisplayName = "OrderDetailService: 查询一个明细单")]
        public void GetOrderDetailServiceTest()
        {
            this.MockEmptyEntity<Order>(o =>
            {
                o.No = "1";
                o.ClientName = "lele";
                o.Status = (int)OrderStatusEnum.Pending;
            });

            this.MockEmptyEntity<OrderDetail>(o =>
            {
                o.OrderNo = "1";
                o.RowNo = 1;
                o.Amount = 500;
                o.MaterialNo = "100";
                o.Unit = "10";
            });

            var result = (JsonResult)this._orderDetailService.QueryDetail("1", 1);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact(DisplayName = "OrderDetailService: 查询一个销售单下的明细单")]
        public void GetOrderDetailServiceOfUserTest()
        {
            this.MockEmptyEntity<Order>(o =>
            {
                o.No = "1";
                o.ClientName = "lele";
                o.Status = (int)OrderStatusEnum.Pending;
            });

            this.MockEntities<OrderDetail>((o,i) =>
            {
                o.OrderNo = "1";
                o.RowNo = i;
                o.Amount = 500;
                o.MaterialNo = "100";
                o.Unit = "10";
            }, 100);
            var result = (JsonResult)this._orderDetailService.QueryDetail("1", default, default);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact(DisplayName = "OrderDetailService: 删除一个明细单")]
        public void DeleteOrderDetailServiceTest()
        {
            this.MockEmptyEntity<Order>(o =>
            {
                o.No = "1";
                o.ClientName = "lele";
                o.Status = (int)OrderStatusEnum.Pending;
            });

            this.MockEmptyEntity<OrderDetail>(o =>
            {
                o.OrderNo = "1";
                o.RowNo = 1;
                o.Amount = 500;
                o.MaterialNo = "100";
                o.Unit = "10";
            });

            var result = (JsonResult)this._orderDetailService.DeleteDetail("1", 1);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact(DisplayName = "OrderDetailService: 删除一个销售单的明细单")]
        public void DeleteOrderDetailServiceOfUserTest()
        {
            this.MockEmptyEntity<Order>(o =>
            {
                o.No = "1";
                o.ClientName = "lele";
                o.Status = (int)OrderStatusEnum.Pending;
            });

            this.MockEmptyEntity<OrderDetail>(o =>
            {
                o.OrderNo = "1";
                o.RowNo = 1;
                o.Amount = 500;
                o.MaterialNo = "100";
                o.Unit = "10";
            });

            var result = (JsonResult)this._orderDetailService.DeleteDetail("1");
            Assert.Equal(200, result.StatusCode);
        }

        [Fact(DisplayName = "OrderDetailService: 更新一个明细单")]
        public void UpdateOrderDetailServiceTest()
        {
            this.MockEmptyEntity<Order>(o =>
            {
                o.No = "1";
                o.ClientName = "lele";
                o.Status = (int)OrderStatusEnum.Pending;
            });

            this.MockEmptyEntity<OrderDetail>(o =>
            {
                o.OrderNo = "1";
                o.RowNo = 1;
                o.Amount = 500;
                o.MaterialNo = "100";
                o.Unit = "10";
            });

            var detailDto = new OrderDetailDto();
            detailDto.Comment = "update";

            var result = (JsonResult)this._orderDetailService.UpdateDetail("1", 1, detailDto);
            Assert.Equal(200, result.StatusCode);
        }


        //OrderUserService
        [Fact(DisplayName = "OrderUserService: 获取一个用户的明细单")]
        public void GetOrderUserServiceTest()
        {
            this.MockEmptyEntity<Order>(o =>
            {
                o.No = "1";
                o.ClientName = "lele";
                o.Status = (int)OrderStatusEnum.Pending;
            });

            this.MockEmptyEntity<OrderDetail>(o =>
            {
                o.OrderNo = "1";
                o.RowNo = 1;
                o.Amount = 500;
                o.MaterialNo = "100";
                o.Unit = "10";
            });

            var result = (JsonResult)this._orderUserService.QueryDetailByUser(default, default);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact(DisplayName = "OrderUserService: 删除一个用户的销售单")]
        public void DeleteOrderUserServiceTest()
        {
            this.MockEmptyEntity<Order>(o =>
            {
                o.No = "1";
                o.ClientName = "lele";
                o.Status = (int)OrderStatusEnum.Pending;
            });

            this.MockEmptyEntity<OrderDetail>(o =>
            {
                o.OrderNo = "1";
                o.RowNo = 1;
                o.Amount = 500;
                o.MaterialNo = "100";
                o.Unit = "10";
            });

            var result = (JsonResult)this._orderUserService.DeleteDetailByUser();
            Assert.Equal(200, result.StatusCode);
        }

        [Fact(DisplayName = "OrderUserService: 获取一个用户的销售单")]
        public void GetOrderYserDetailServiceTest()
        {
            this.MockEmptyEntity<Order>(o =>
            {
                o.No = "1";
                o.ClientName = "lele";
                o.Status = (int)OrderStatusEnum.Pending;
            });

            var result = (JsonResult)this._orderUserService.QureyOrderByUser(default, default);
            Assert.Equal(200, result.StatusCode);
        }
    }


}
