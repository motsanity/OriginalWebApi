using MediatR;

namespace webapi.CQRS.Command.CommandCartItem
{
    public class UpdateCartItemCommand: IRequest<Unit> //added 4:33pm 1/24/2023
    {
        public Guid CartItemId { get; set; }   
        public string? CartItemName { get; set; }

    }
}
 