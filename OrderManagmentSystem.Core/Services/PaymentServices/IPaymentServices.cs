using OrderManagmentSystem.Core.Entites;
using OrderManagmentSystem.Core.Entites.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagmentSystem.Core.Services.PaymentServices
{
    public interface IPaymentServices
    {
        
        Task ProcessPayment(Order order);

    }
}
