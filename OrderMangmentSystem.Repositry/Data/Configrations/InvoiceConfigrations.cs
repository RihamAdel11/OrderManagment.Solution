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
	public class InvoiceConfigrations : IEntityTypeConfiguration<Invoice>
	{
		public void Configure(EntityTypeBuilder<Invoice> builder)
		{
			builder.HasOne(V=>V.Order).WithMany().HasForeignKey (v=>v.OrderId);
			builder.Property(v => v.TotalAmount).HasColumnType("decimal(18,2)");
		}
	}
}
