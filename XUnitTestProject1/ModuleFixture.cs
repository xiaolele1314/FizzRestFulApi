using System;
using System.Linq;
using Fizz.SalesOrder.Models;
using Fizz.SalesOrder.Service;
using Fizz.SalesOrder;
using Microsoft.Extensions.DependencyInjection;
using Fizz.SalesOrder.Interface;
using Fizz.SalesOrder.Controllers;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Test.MockService;
using System.Reflection;

namespace Test
{
    public class ModuleFixture:IDisposable
    {
        public IServiceProvider Services { get; set; }

        public SalesContext SalesContext { get; private set; }

        public DbContextOptions<SalesContext> Options { get; private set; }

        public ModuleFixture()
        {

            var startupAssembly = typeof(Startup).Assembly;

            var services = new ServiceCollection();

            services.AddDbContext<SalesContext>(opt =>
            {
                opt.UseInMemoryDatabase("FizzSalesOrderUnitTestDatabase");
                this.Options = opt.Options as DbContextOptions<SalesContext>;
            });

            //services.AddDbContext<SalesContext>(options => {
            //    options.UseMySQL("server=service.byzan.cxist.com;userid=root;password=Iubang001!;database=Dev.fizz.Ord;");
            //    this.Options = options.Options as DbContextOptions<SalesContext>;
            //    });

            //services.AddMvc(options =>
            //{
            //    options.EnableEndpointRouting = false;
            //})
            //    .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);


            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderDetailService, OrderDetailService>();
            services.AddScoped<IOrderUserService, OrderUserService>();
            services.AddScoped<IUserService, UserService>();
            services.AddSingleton<UserMiddleware>();
            services.AddAutoMapper(typeof(AutoMapperConfigs));

            services.AddControllers()
                .AddApplicationPart(startupAssembly)
                .AddControllersAsServices()
                .AddNewtonsoftJson(option => option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddSingleton(a => new OrderController(new MockOrderService()));
            this.Services = services.BuildServiceProvider();

            Console.WriteLine("test begin");
        }

       

        public void Reset()
        {
            
            if (this.SalesContext == null)
            {
                this.SalesContext = this.Services.GetRequiredService<SalesContext>();
            }


            this.ClearData();

            foreach (var entry in this.SalesContext.ChangeTracker.Entries().ToArray())
            {
                if (entry.State == EntityState.Detached)
                {
                    continue;
                }
                entry.State = EntityState.Detached;
            }
        }

        public void ClearData()
        {
            var entityTypes = this.SalesContext.Model.GetEntityTypes();
            foreach(var eType in entityTypes)
            {
                var clearMethod = this.GetType()
                    .GetMethod("ClearEntityData", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                    .MakeGenericMethod(eType.ClrType);
                clearMethod.Invoke(this, null);
            }
            
        }

        public  void ClearEntityData<TEntity>()
            where TEntity : class
        {
            var set = this.SalesContext.Set<TEntity>();
            var query = set.Where(a => true);
            var entities = query.ToList();
            if (entities.Count == 0)
            {
                return;
            }

            foreach (var entity in entities)
            {
                this.SalesContext.Remove(entity);
            }

            this.SalesContext.SaveChanges(true);
            
        }
        public SalesContext CreatContext()
        {
            var context = Activator.CreateInstance(typeof(SalesContext), new object[] { this.Options, new UserService() { } }) as SalesContext;
            
            return context ?? default;
        }
        public void Dispose()
        {           
            Console.WriteLine("test finish");
        }
    }
}
