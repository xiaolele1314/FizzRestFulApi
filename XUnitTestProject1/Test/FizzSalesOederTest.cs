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
        [Fact(DisplayName = "OrderController: ��ȡ�������۶���")]
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

        [Fact(DisplayName = "OrderController: ��ȡ�������۶���")]
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

        [Fact(DisplayName = "OrderController: ɾ���������۶���")]
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

        [Fact(DisplayName = "OrderController: ���ĵ������۶���")]
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

        [Fact(DisplayName = "OrderController: �������۶���")]
        public void AddOrderTest()
        {
            var order = new Order();
            order.No = "1";
            order.ClientName = "lele";

            var result = (JsonResult)this._orderController.Post(order);
            Assert.Equal(200, result.StatusCode);
        }


        //OrderDetailController
        [Fact(DisplayName = "OrderDetailController: ��ȡ������ϸ��")]
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

        [Fact(DisplayName = "OrderDetailController: ��ȡ���۵��µ�������ϸ��")]
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

        [Fact(DisplayName = "OrderDetailController: ɾ��һ����ϸ��")]
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

        [Fact(DisplayName = "OrderDetailController: ɾ�����۵��µ�������ϸ��")]
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

        [Fact(DisplayName = "OrderDetailController: ������ϸ��")]
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

        [Fact(DisplayName = "OrderDetailController: ������ϸ��")]
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
        [Fact(DisplayName = "OrderUserController: ɾ���û��µ�������ϸ��")]
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

        [Fact(DisplayName = "OrderUserController: ��ȡ�û��µ�������ϸ��")]
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

        [Fact(DisplayName = "OrderUserController: ��ȡ�û��µ��������۵�")]
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
        [Fact(DisplayName = "OrderService: �������۵�")]
        public void CreateOrderServiceTest()
        {
            var order = new Order();
            order.No = "1";
            order.ClientName = "lele";
            var result = (JsonResult)this._orderService.CreatOrder(order);

          
            
            Assert.Equal(200, result.StatusCode);
            
        }

        [Fact(DisplayName = "OrderService: ��ѯһ�����۵�")]
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

        [Fact(DisplayName = "OrderService: ��ѯ�������۵�")]
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

        [Fact(DisplayName = "OrderService: ɾ��һ�����۵�")]
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

        [Fact(DisplayName = "OrderService: ����һ�����۵�")]
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
        [Fact(DisplayName = "OrderDetailService: ������ϸ��")]
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

        [Fact(DisplayName = "OrderDetailService: ��ѯһ����ϸ��")]
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

        [Fact(DisplayName = "OrderDetailService: ��ѯһ�����۵��µ���ϸ��")]
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

        [Fact(DisplayName = "OrderDetailService: ɾ��һ����ϸ��")]
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

        [Fact(DisplayName = "OrderDetailService: ɾ��һ�����۵�����ϸ��")]
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

        [Fact(DisplayName = "OrderDetailService: ����һ����ϸ��")]
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
        [Fact(DisplayName = "OrderUserService: ��ȡһ���û�����ϸ��")]
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

        [Fact(DisplayName = "OrderUserService: ɾ��һ���û������۵�")]
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

        [Fact(DisplayName = "OrderUserService: ��ȡһ���û������۵�")]
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
