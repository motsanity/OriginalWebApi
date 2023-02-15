using AutoMapper;
using webapi.AppService.DTO.DTOCartItem;
using webapi.AppService.DTO.DTOCheckout;
using webapi.CQRS.Command.CommandCartItem;
using webapi.CQRS.Command.CommandCheckout;
using webapi.CQRS.ViewModels;
using webapi.Domain.Models;
using webapi.Infrastructure.Database.Entities;


namespace webapi.AppService.Profiles
{
    public class CheckoutProfile : Profile //added 5:21pm 1/24/2023
    {
        public CheckoutProfile()
        {
            CreateMap<webapi.Domain.Models.CheckoutModel, webapi.Infrastructure.Database.Entities.Checkout>();
            CreateMap<CheckoutOrderDTO, CheckoutOrderCommand>();
            CreateMap<CheckoutOrderCommand, CheckoutModel>()
                .ConstructUsing((s) => new CheckoutModel(s.CustomerId, s.OrderPrimaryId, s.Status)); //return constructor 2/14/2023

            CreateMap<CheckoutModel, CheckoutViewModel>();
            CreateMap<UpdateCartItemDTO, UpdateCartItemCommand>();

            CreateMap<CheckoutModel, Infrastructure.Database.Entities.Checkout>()
                .ForMember(d => d.Status, o => o.MapFrom(s => (short)s.Status));
            CreateMap<Infrastructure.Database.Entities.Checkout, CheckoutModel>();
            CreateMap<Infrastructure.Database.Entities.Order, CheckoutModel>();
            CreateMap<Infrastructure.Database.Entities.Checkout, Order>();





        }



    }
}
