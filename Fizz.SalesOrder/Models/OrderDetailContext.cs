﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fizz.SalesOrder.Models
{
    public class OrderDetailContext:DbContext
    {
        public OrderDetailContext(DbContextOptions<OrderDetailContext> options)
           : base(options)
        {

        }
     
        public DbSet<OrderDetail> orderDetails { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            modelBuilder.Entity<OrderDetail>()
                .HasOne(d => d.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(d => d.OrderNo);

            

            modelBuilder.Entity<OrderDetail>()
                .HasIndex(o => new { o.RowNo,o.OrderNo })
                .IsUnique();



            base.OnModelCreating(modelBuilder);
        }
    }
}
