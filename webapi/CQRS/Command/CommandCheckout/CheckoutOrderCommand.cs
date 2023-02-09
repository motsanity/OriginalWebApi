using MediatR;

namespace webapi.CQRS.Command.CommandCheckout
{
    public class CheckoutOrderCommand : IRequest<Guid>
    {
          //added 4:33pm 1/24/2023
            public Guid CustomerId { get; set; }
            public Guid OrderPrimaryId { get; set; }
            public short Status { get; set; }


        
    }
}
