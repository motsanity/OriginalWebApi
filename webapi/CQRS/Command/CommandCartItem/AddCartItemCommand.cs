using MediatR;

namespace webapi.CQRS.Command.CommandCartItem
{
    public class AddCartItemCommand: IRequest<Guid> //added 4:33pm 1/24/2023
    {
        public string? CartItemName { get; set; }
        public Guid CustomerId { get; set; }
        public Guid OrderPrimaryId { get; internal set; } //added 2/14/2023
    }
}
 