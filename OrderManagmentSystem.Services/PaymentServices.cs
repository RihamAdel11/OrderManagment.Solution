using Microsoft.Extensions.Configuration;
using OrderManagmentSystem.Core;
using OrderManagmentSystem.Core.Entites;
using OrderManagmentSystem.Core.Entites.Payments;
using OrderManagmentSystem.Core.Services.PaymentServices;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Customer = OrderManagmentSystem.Core.Entites.Customer;
using PaymentMethod = OrderManagmentSystem.Core.Entites.Payments.PaymentMethod;

namespace OrderManagmentSystem.Services
{
	public class PaymentServices : IPaymentServices
	{
		private readonly IConfiguration _configration;
		
		private readonly IUnitOfWork _unitofWork;

		public PaymentServices(IConfiguration configration,  IUnitOfWork unitofWork)
		{
			_configration = configration;
		
			_unitofWork = unitofWork;
		}
		public async Task ProcessPayment(Order order)
		{
			switch(order.PaymentMethod)
			{
				case PaymentMethod.CreditCard :
					await ProcessCreditPayment(order);
					break;
				case PaymentMethod.PayPal:
					await ProcessPaypalPayment(order); break;
				case PaymentMethod.BankTransfere:
					await ProcessBankTransferePayment(order); break;
				default:
					throw new ArgumentException("Invalid PayMent Method");

			}
		}
		private async Task ProcessCreditPayment(Order order)
		{
			//implement credit card payment Processing logic
		}
		private async Task ProcessPaypalPayment(Order order)
		{
			//implement Paypal payment Processing logic
		}
		private async Task ProcessBankTransferePayment(Order order)
		{
			//implement Bank transfere payment Processing logic
		}

		

			
			
		}
	}

