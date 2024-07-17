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
	public class ProductConfigrations : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder.Property (p=>p.Name ).IsRequired ();
			builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
		}
	}
}
