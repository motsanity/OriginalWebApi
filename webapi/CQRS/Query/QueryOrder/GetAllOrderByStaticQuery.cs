using MediatR;
using webapi.CQRS.ViewModels;

namespace webapi.CQRS.Query.QueryOrder
{
    public class GetAllOrderByStaticQuery : IRequest<IEnumerable<OrderViewModel>>
    {
    }
}
