using MediatR;
using webapi.CQRS.ViewModels;



namespace webapi.CQRS.Query.QueryOrder
{
    public class GetAllOrdersQuery: IRequest<IEnumerable<OrderViewModel>>
    {
    }
}
