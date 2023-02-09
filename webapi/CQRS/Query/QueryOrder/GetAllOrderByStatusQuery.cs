using MediatR;
using webapi.CQRS.ViewModels;

namespace webapi.CQRS.Query.QueryOrder
{
    public class GetAllOrderByStatusQuery : IRequest<IEnumerable<OrderViewModel>>
    {
        public short Status { get; set; }
    }
}
