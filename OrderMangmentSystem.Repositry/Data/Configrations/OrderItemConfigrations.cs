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
	public class OrderItemConfigrations : IEntityTypeConfiguration<OrderItem>
	{
		public void Configure(EntityTypeBuilder<OrderItem> builder)
		{
			builder.HasOne(I=>I.Order).WithMany().HasForeignKey(I=>I.OrderId);
			builder.Property(o => o.UnitPrice).HasColumnType("decimal(18,2)");
			builder.Property(o => o.Discount ).HasColumnType("decimal(18,2)");
		}
	}
}
