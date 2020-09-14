using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Fizz.SalesOrder.Models;
using Fizz.SalesOrder.Service;
using AutoMapper;
using Fizz.SalesOrder.Interface;
using Fizz.SalesOrder.Controllers;

namespace Fizz.SalesOrder
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
         
            services.AddDbContext<SalesContext>(options => options.UseMySQL(Configuration.GetConnectionString("dbserver")));


            //services.AddMvc(options => { 
            //        options.EnableEndpointRouting = false;
            //    })   
            //    .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            //services.AddControllers();

            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderDetailService, OrderDetailService>();
            services.AddScoped<IOrderUserService, OrderUserService>();
            services.AddScoped<IUserService, UserService>();
            services.AddSingleton<UserMiddleware>();
            services.AddAutoMapper(typeof(AutoMapperConfigs));

            services.AddControllers().AddNewtonsoftJson(option => option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, SalesContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }else
            {
                app.UseHsts();
            }

            //app.UseHttpsRedirection();

            app.UseMiddleware<UserMiddleware>();

            //app.UseMvc();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            context.Database.EnsureCreated();

        }
    }
}
