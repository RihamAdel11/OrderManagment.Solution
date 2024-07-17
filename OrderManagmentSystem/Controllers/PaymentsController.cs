using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagmentSystem.Core.Entites.Payments;
using OrderManagmentSystem.Core.Services.PaymentServices;

namespace OrderManagmentSystem.Controllers
{
    [Authorize ]
	public class PaymentsController : ApiBaseController 
	{
		[HttpPost("process")]
		public IActionResult ProcessPayment([FromBody] PaymentRequest request)
		{
			if (request == null || string.IsNullOrEmpty(request.paymentMethod))
			{
				return BadRequest("Invalid payment request.");
			}

			switch (request.paymentMethod.ToLower())
			{
				case "creditcard":
					return ProcessCreditCardPayment(request.Amount );
				case "paypal":
					return ProcessPayPalPayment(request.Amount);
				case "banktransfer":
					return ProcessBankTransferPayment(request.Amount);
				default:
					return BadRequest("Unsupported payment method.");
			}
		}

		private IActionResult ProcessCreditCardPayment(decimal amount)
		{
			// Implement credit card payment processing logic here
			return Ok($"Processed credit card payment of {amount:C2}");
		}

		private IActionResult ProcessPayPalPayment(decimal amount)
		{
			// Implement PayPal payment processing logic here
			return Ok($"Processed PayPal payment of {amount:C2}");
		}

		private IActionResult ProcessBankTransferPayment(decimal amount)
		{
			// Implement bank transfer payment processing logic here
			return Ok($"Processed bank transfer payment of {amount:C2}");
		}
	}
}
	
