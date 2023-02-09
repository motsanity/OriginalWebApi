using AutoMapper;
using MediatR;
using webapi.CQRS.Command.CommandUser;
using webapi.Domain.Contracts;
using webapi.Domain.Models; //should be model and not entity

namespace webapi.CQRS.CommandHandlers
{
    //Multiple User Command Handler
    public class UserCommandHandler : IRequestHandler<AddUserCommand, Guid>,
        IRequestHandler<UpdateUserCommand> //added 1:16PM 1/24/2023
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<UserModel>(request);

            return await _userRepository.AddUser(user);

        }
        //added 1:17PM 1/24/2023
        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            await _userRepository.UpdateUser(request.UserId, request.UserName);

            return new Unit();
            
        }
    }
}
