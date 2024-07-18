using Moq;
using OrderManagmentSystem.Core;
using OrderManagmentSystem.Core.Entites;
using OrderManagmentSystem.Core.Entites.Payments;
using OrderManagmentSystem.Core.Repositries;
using OrderManagmentSystem.Services;
using OrderMangmentSystem.Repositry;
using Stripe.Climate;
using Order = OrderManagmentSystem.Core.Entites.Order;

namespace OrderManagmentSystem.Test
{
	public class OrderServicesTest
	{
		[Fact]
		public async Task CreateOrder_ShouldCreateOrder_WhenValid()
		{
			// Arrange
			var order = new Core.Entites.Order
            {
				CustomerId = 1,
				OrderDate = DateTime.Now,
				TotalAmount = 100m,
				PaymentMethod = PaymentMethod.CreditCard,
				Status = OrderStatus.Pending
			};

			var mockUnitOfWork = new Mock<IUnitOfWork>();
			mockUnitOfWork.Setup(x => x.Repositry <Order>().AddAsync(It.IsAny<Order>()))
				.Returns(Task.CompletedTask);
			mockUnitOfWork.Setup(x => x.CompleteAsync())
				.Returns((Task<int>)Task.CompletedTask);

			var orderService = new OrderServices(mockUnitOfWork.Object ,null);

			// Act
			var createdOrder =  orderService.CreateOrderAsync(order);

			// Assert
			Assert.NotNull(createdOrder);
			mockUnitOfWork.Verify(x => x.Repositry<Order>().AddAsync(order), Times.Once);
			mockUnitOfWork.Verify(x => x.CompleteAsync(), Times.Once);
		}
	}


}
