using AutoMapper;
using webapi.CQRS.ViewModels;
using webapi.Domain.Models;
using webapi.Infrastructure.Database.Entities;


namespace webapi.AppService.Profiles
{
    public class OrderProfile : Profile //added 5:21pm 1/24/2023
    {
        public OrderProfile()
        {
          
            CreateMap<OrderModel, OrderViewModel>();

            //4:45pm 1/30/2023
            CreateMap<OrderModel, CartItem>();
            CreateMap<Order, CartItem>();
            CreateMap<Order, CartItemModel>();
            CreateMap<OrderModel, CartItemModel>();

            CreateMap<OrderModel, Infrastructure.Database.Entities.Order>()
                .ForMember(d => d.Status, o => o.MapFrom(s => (short)s.Status));
            CreateMap<Infrastructure.Database.Entities.Order, OrderModel>();




        }
    }
}
