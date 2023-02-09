using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Infrastructure.Database.Entities
{
    public class Checkout //new status 2:04 1/31/2023
    {
        public Guid CheckoutId {get;set;}
        public Guid CustomerId { get; set; }

        [ForeignKey("Order")]
        public Guid OrderPrimaryId { get; set; }
        public virtual Order Order { get; set; }

        public short Status { get; set; }

    }
}
