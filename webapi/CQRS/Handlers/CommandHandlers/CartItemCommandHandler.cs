using AutoMapper;
using MediatR;
using webapi.CQRS.Command.CommandCartItem;
using webapi.Domain.Contracts;
using webapi.Domain.Models; //should be model and not entity

namespace webapi.CQRS.CommandHandlers
{
    //Multiple CartItem Command Handler 
    public class CartItemCommandHandler : IRequestHandler<AddCartItemCommand, Guid>,
        IRequestHandler<UpdateCartItemCommand>,
        IRequestHandler<DeleteCartItemCommand>
    {
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IMapper _mapper;

        public CartItemCommandHandler(ICartItemRepository cartItemRepository, IMapper mapper)
        {
            _cartItemRepository = cartItemRepository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(AddCartItemCommand request, CancellationToken cancellationToken)
        {
            var cartItem = new CartItemModel
            {
                CartItemName = request.CartItemName,
                CustomerId = request.CustomerId
            };
            return await _cartItemRepository.AddCartItem(cartItem);
            //var cartitem = _mapper.Map<CartItemModel>(request);

            //return await _cartItemRepository.AddCartItem(cartitem);

        }
        
        public async Task<Unit> Handle(UpdateCartItemCommand request, CancellationToken cancellationToken)
        {
            await _cartItemRepository.UpdateCartItem(request.CartItemId, request.CartItemName);

            return new Unit();
            
        }
        public async Task<Unit> Handle(DeleteCartItemCommand request, CancellationToken cancellationToken)
        {
            await _cartItemRepository.DeleteCartItem(request.CartItemId);
            return new Unit();
        }


    }
}
