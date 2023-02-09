using MediatR;

namespace webapi.CQRS.Command.CommandUser
{
    public class UpdateUserCommand: IRequest<Unit> //added 1:15pm 1/24/2023 //edited 2:46pm 1/24/2023 from Guid to Unit
    {
        public Guid UserId { get; set; }   //added 1:20PM 1/24/2023
        public string? UserName { get; set; }

    }
}
 