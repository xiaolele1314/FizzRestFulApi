using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Models
{
    public class OrderContext:DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options)
            : base(options)
        {

        }

        public DbSet<Order> orders { get; set; }
        public DbSet<OrderDetail> orderDetails { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasIndex(o => o.No)
                .IsUnique();

            modelBuilder.Entity<OrderDetail>()
                .HasOne(d => d.order)
                .WithMany(o => o.orderDetails)
                .HasForeignKey(d => d.No);

            modelBuilder.Entity<OrderDetail>()
                .HasIndex(o => o.ProNo)
                .IsUnique();
                
                

            base.OnModelCreating(modelBuilder);
        }
    }
}
