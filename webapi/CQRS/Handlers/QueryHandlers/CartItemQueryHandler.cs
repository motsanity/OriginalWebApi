using AutoMapper;
using MediatR;
using webapi.CQRS.Query.QueryItem;
using webapi.CQRS.ViewModels;
using webapi.Domain.Contracts;


namespace webapi.CQRS.QueryHandlers
{
    public class CartItemQueryHandler : IRequestHandler<GetAllCartItemsQuery, List<CartItemViewModel>>
    {
        private readonly IMapper _mapper;
        private readonly ICartItemRepository _cartItemRepository;

        public CartItemQueryHandler(ICartItemRepository cartItemRepository, IMapper mapper)
        {
            _cartItemRepository = cartItemRepository;
            _mapper = mapper;
        }

        public async Task<List<CartItemViewModel>> Handle(GetAllCartItemsQuery request, CancellationToken cancellationToken)
        {
            var cartitems = await _cartItemRepository.GetAllCartItems();

            return _mapper.Map<List<CartItemViewModel>>(cartitems);
        }

        
    }
}
