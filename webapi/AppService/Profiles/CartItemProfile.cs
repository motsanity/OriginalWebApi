using AutoMapper;
using webapi.AppService.DTO.DTOCartItem;
using webapi.CQRS.Command.CommandCartItem;
using webapi.CQRS.ViewModels;
using webapi.Domain.Models;


namespace webapi.AppService.Profiles
{
    public class CartItemProfile : Profile //added 5:21pm 1/24/2023
    {
        public CartItemProfile()
        {
            CreateMap<webapi.Domain.Models.CartItemModel, webapi.Infrastructure.Database.Entities.CartItem>();
            CreateMap<AddCartItemDTO, AddCartItemCommand>();
            CreateMap<AddCartItemCommand, CartItemModel>()
                .ConstructUsing((s) => new CartItemModel(s.CartItemName, s.CustomerId, s.OrderPrimaryId)); // return constructor 2/14/2023

            CreateMap<CartItemModel, CartItemViewModel>();

            CreateMap<UpdateCartItemDTO, UpdateCartItemCommand>();

            



        }

        

    }
}
