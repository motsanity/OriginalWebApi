using AutoMapper;
using MediatR;
using webapi.CQRS.ViewModels;
using webapi.Domain.Contracts;
using webapi.CQRS.Query.QueryOrder;

namespace webapi.CQRS.QueryHandlers
{
    public class OrderQueryHandler : IRequestHandler<GetAllOrdersQuery, IEnumerable<OrderViewModel>>,
       IRequestHandler<GetOrderByIdQuery, OrderViewModel>,
        IRequestHandler<GetAllOrderByStatusQuery, IEnumerable<OrderViewModel>>,
        IRequestHandler<GetAllOrderByStaticQuery, IEnumerable<OrderViewModel>>//from list
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;

        public OrderQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        //from list
        public async Task<IEnumerable<OrderViewModel>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetAllOrders();

            return _mapper.Map<IEnumerable<OrderViewModel>>(orders);


        }

        public async Task<OrderViewModel> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrderById(request.OrderId);

            return _mapper.Map<OrderViewModel>(order);
        }

        public async Task<IEnumerable<OrderViewModel>> Handle(GetAllOrderByStatusQuery request, CancellationToken cancellationToken)
        {
            var status = await _orderRepository.GetAllOrderByStatus(request.Status);

            return _mapper.Map<IEnumerable<OrderViewModel>>(status);
        }

        public async Task<IEnumerable<OrderViewModel>> Handle(GetAllOrderByStaticQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetAllOrderByStatic();

            return _mapper.Map<IEnumerable<OrderViewModel>>(orders);
        }
    }
}
