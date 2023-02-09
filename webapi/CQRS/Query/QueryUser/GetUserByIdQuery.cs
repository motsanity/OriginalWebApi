using MediatR;
using webapi.CQRS.ViewModels;

namespace webapi.CQRS.Query.QueryUser
{
    public class GetUserByIdQuery : IRequest<UserViewModel>
    {
        public Guid UserId { get; set; }
    }
}
