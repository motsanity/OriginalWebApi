using MediatR;
using webapi.CQRS.ViewModels;

namespace webapi.CQRS.Query.QueryItem
{
    public class GetAllCartItemsQuery : IRequest<List<CartItemViewModel>>
    {

    }
}
