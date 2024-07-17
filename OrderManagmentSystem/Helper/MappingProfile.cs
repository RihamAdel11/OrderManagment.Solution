using AutoMapper;
using OrderManagmentSystem.Core.Entites;
using OrderManagmentSystem.DTOs;

namespace OrderManagmentSystem.Helper
{
	public class MappingProfile:Profile
	{
        public MappingProfile()
        {
            CreateMap<Invoice, InvoiceDto>().ForMember(d=>d.Order,o=>o.MapFrom (s=>s.Order.Name));
			CreateMap<Order, OrderDto>().ForMember(d => d.Customer, o => o.MapFrom(s => s.Customer.Name));
			CreateMap<OrderItem ,OrderItemDto >().ForMember(d => d.Order, o => o.MapFrom(s => s.Order.Name));
		}
    }
}
