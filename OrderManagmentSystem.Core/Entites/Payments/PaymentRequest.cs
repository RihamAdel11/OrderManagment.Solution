using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagmentSystem.Core.Entites.Payments
{
    public class PaymentRequest
    {
        public PaymentRequest()
        {

        }
        public decimal Amount { get; set; }
        public string paymentMethod { get; set; }
        public int CustomerId { get; set; }

        public PaymentRequest(int customerId, decimal Amount, string _paymentMethoid)
        {
            customerId = CustomerId;
           Amount = Amount;
            paymentMethod = _paymentMethoid;
        }
    }
}

