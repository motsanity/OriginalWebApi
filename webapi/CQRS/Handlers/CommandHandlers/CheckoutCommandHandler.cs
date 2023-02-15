using AutoMapper;
using MediatR;
using webapi.CQRS.Command.CommandCheckout;
using webapi.Domain.Contracts;
using webapi.Domain.Models; //should be model and not entity

namespace webapi.CQRS.CommandHandlers
{
    //Multiple CartItem Command Handler 
    public class CheckoutCommandHandler : IRequestHandler<CheckoutOrderCommand, Guid>
    {
        private readonly ICheckoutRepository _checkoutRepository;
        private readonly IMapper _mapper;

        public CheckoutCommandHandler(ICheckoutRepository checkoutRepository, IMapper mapper)
        {
            _checkoutRepository = checkoutRepository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            //var checkout = new CheckoutModel
            //{
            //    CustomerId = request.CustomerId,
            //    OrderPrimaryId = request.OrderPrimaryId,
            //    Status = (Domain.Enumerations.Status)request.Status
            //};

            //replaced this from the top 2/14/2023
            var checkout = _mapper.Map<CheckoutModel>(request);
            return await _checkoutRepository.CheckoutOrder(checkout);
        }
    }
}
