using AutoMapper;
using webapi.AppService.DTO.DTOUser;
using webapi.CQRS.Command.CommandUser;
using webapi.CQRS.Query.QueryUser;
using webapi.CQRS.ViewModels;
using webapi.Domain.Models;
using webapi.Infrastructure.Database.Entities;

namespace webapi.AppService.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<webapi.Domain.Models.UserModel, webapi.Infrastructure.Database.Entities.User>();
            CreateMap<AddUserDTO, AddUserCommand>();
            CreateMap<AddUserCommand, UserModel>();
                //.ConstructUsing((s) => new UserModel(s.UserName)); 5:30pm 1/27/2023

            CreateMap<UserModel, UserViewModel>();

            CreateMap<UpdateUserDTO, UpdateUserCommand>(); //added 1:53pm 1/24/2023

            CreateMap<UserModel, OrderModel>();
            CreateMap<UserModel, Order>();
            CreateMap<User, OrderModel>();
            CreateMap<User, Order>();
            CreateMap<UserModel, User>();

            CreateMap<GetUserByIdQuery, UserModel>();
            CreateMap<GetUserByIdQuery, User>();
            CreateMap<GetUserByIdQuery, Order>();
            CreateMap<GetUserByIdQuery, OrderModel>();
            




        }

        

    }
}
