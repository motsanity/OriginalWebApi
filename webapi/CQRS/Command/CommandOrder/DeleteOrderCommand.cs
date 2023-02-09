using MediatR;

namespace webapi.CQRS.Command.CommandOrder
{
    public class DeleteOrderCommand: IRequest
    {
        public Guid OrderId { get; set; }
    }
}
