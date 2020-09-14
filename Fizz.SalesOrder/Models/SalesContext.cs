using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;
using Fizz.SalesOrder.Interface;

namespace Fizz.SalesOrder.Models
{
    public class SalesContext:DbContext
    {
        private readonly IUserService userService;

        public static readonly LoggerFactory LoggerFactory =
               new LoggerFactory(new[] { new DebugLoggerProvider() });

        public SalesContext(DbContextOptions<SalesContext> options, IUserService userService)
            : base(options)
        {
            this.userService = userService;
        }
        public SalesContext(DbContextOptions<SalesContext> options)
        : base(options)
        {
            
        }

        

        //重写savechanges方法
        public override int SaveChanges()
        {
            DateTime nowTime = DateTime.Now;
            this.ChangeTracker.DetectChanges();

            foreach (var entity in this.ChangeTracker.Entries())
            {
                if (entity.State == EntityState.Added)
                {
                    entity.Property("CreateUserNo").CurrentValue = this.userService.getUser();
                    entity.Property("CreateUserDate").CurrentValue = nowTime;
                    entity.Property("UpdateUserNo").CurrentValue = this.userService.getUser();
                    entity.Property("UpdateUserDate").CurrentValue = nowTime;
                }

                if (entity.State == EntityState.Modified)
                {

                    entity.Property("UpdateUserNo").CurrentValue = this.userService.getUser();
                    entity.Property("UpdateUserDate").CurrentValue = nowTime;
                }
            }
            return base.SaveChanges();
        }

        public DbSet<Order> orders { get; set; }
        public DbSet<OrderDetail> details { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasIndex(o => o.No)
                .IsUnique();

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(o => new { o.RowNo, o.OrderNo });

                entity.HasOne(d => d.Order)
                    .WithMany(o => o.OrderDetails)
                    .HasForeignKey(d => d.OrderNo);

                entity.HasIndex(o => new { o.RowNo, o.OrderNo })
                    .IsUnique();
            });

            base.OnModelCreating(modelBuilder);
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLoggerFactory(LoggerFactory);
        }
    }
}
