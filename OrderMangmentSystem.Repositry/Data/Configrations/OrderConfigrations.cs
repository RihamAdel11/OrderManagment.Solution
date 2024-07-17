using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManagmentSystem.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMangmentSystem.Repositry.Data.Configrations
{
	public class OrderConfigrations : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			builder.Property(o => o.Status).HasConversion(OStatus => OStatus.ToString(), OStatus =>(OrderStatus ) Enum.Parse(typeof(OrderStatus), OStatus));
			builder.HasOne(o=>o.Customer).WithMany()
				.HasForeignKey(o=>o.CustomerId);
			builder.Property(o => o.TotalAmount).HasColumnType("decimal(18,2)");

		}
	}
}
