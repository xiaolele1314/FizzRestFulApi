using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using Google.Protobuf.WellKnownTypes;

namespace TodoApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            users = new List<User>();
        }

        public IConfiguration Configuration { get; }

        //内存中创建用户
        public static List<User> users { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<TodoContext>(opt =>
            //  opt.UseInMemoryDatabase("TodoList"));

            services.AddDbContext<OrderContext>(optios => optios.UseMySQL(Configuration.GetConnectionString("dbconn")));
            services.AddMvc(options => { options.EnableEndpointRouting = false; })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);


            services.AddControllers();

            
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, OrderContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            /*app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });*/

            app.UseMvc();

            context.Database.EnsureCreated();

        }
    }
}
