using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;

namespace Fizz.SalesOrder.Models
{
    public class SalesContext:DbContext
    {
        public static readonly LoggerFactory LoggerFactory =
               new LoggerFactory(new[] { new DebugLoggerProvider() });

        public SalesContext(DbContextOptions<SalesContext> options)
            : base(options)
        {

        }

        public DbSet<Order> orders { get; set; }
        public DbSet<OrderDetail> details { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasIndex(o => o.No)
                .IsUnique();

            modelBuilder.Entity<OrderDetail>()
                .HasKey(o => new { o.RowNo, o.OrderNo });

            modelBuilder.Entity<OrderDetail>()
                .HasOne(d => d.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(d => d.OrderNo);

            modelBuilder.Entity<OrderDetail>()
                .HasIndex(o => new { o.RowNo, o.OrderNo })
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLoggerFactory(LoggerFactory);
        }
    }
}
