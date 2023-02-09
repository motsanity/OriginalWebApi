using MediatR;

namespace webapi.CQRS.Command.CommandUser
{
    public class AddUserCommand: IRequest<Guid>
    {
        public string? UserName { get; set; }

    }
}
 