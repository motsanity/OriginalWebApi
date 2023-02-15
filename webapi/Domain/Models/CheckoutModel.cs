using System.ComponentModel.DataAnnotations.Schema;
using webapi.Domain.Enumerations;
using webapi.Infrastructure.Database.Entities;

namespace webapi.Domain.Models
{
    public class CheckoutModel
    {

        public CheckoutModel( Guid customerId, Guid orderPrimaryId, short status) //added 2/14/2023
        {
            CustomerId = customerId;
            OrderPrimaryId = orderPrimaryId;
            Status = (Status)status;
        }

        //public Guid CheckoutId { get; set; }
        //modified to private and comment out the checkoutid 2/14/2023
        public Guid CustomerId { get; private set; }
        public Guid OrderPrimaryId { get; private set; }
        public Status Status { get; private set; }
    }
}
