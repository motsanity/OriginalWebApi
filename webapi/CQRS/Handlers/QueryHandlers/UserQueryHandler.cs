using AutoMapper;
using MediatR;
using webapi.CQRS.Query.QueryItem;
using webapi.CQRS.ViewModels;
using webapi.Domain.Contracts;
using Dapper;
using webapi.CQRS.Query.QueryUser;

namespace webapi.CQRS.QueryHandlers
{
    public class UserQueryHandler : IRequestHandler<GetUserByIdQuery, UserViewModel>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserViewModel> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserById(request.UserId);

            return _mapper.Map<UserViewModel>(user);
        }

    }
}
