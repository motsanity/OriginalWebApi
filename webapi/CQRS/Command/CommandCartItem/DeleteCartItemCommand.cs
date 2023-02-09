using MediatR;

namespace webapi.CQRS.Command.CommandCartItem
{
    public class DeleteCartItemCommand : IRequest
    {
        public Guid CartItemId { get; set; }
    }
}
