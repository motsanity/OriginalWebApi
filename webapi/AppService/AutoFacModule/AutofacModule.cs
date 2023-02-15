using Autofac;
using AutoMapper;
using FluentValidation;
using MediatR;
using MediatR.Extensions.FluentValidation.AspNetCore;
using System.Reflection;
using webapi.AppService.Profiles;
using webapi.CQRS;
using webapi.CQRS.Command.CommandCartItem;
using webapi.CQRS.Command.CommandCheckout;
using webapi.CQRS.Command.CommandOrder;
using webapi.CQRS.Command.CommandUser;
using webapi.CQRS.Validation.CommandValidator;
using webapi.Domain.Contracts;
using webapi.Infrastructure.Database.Contexts;
using webapi.Infrastructure.Database.Repository;
using webapi.WebAPI.Common;
using Module = Autofac.Module;

namespace webapi.AppService.AutoFacModule
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //Database
            builder.RegisterType<AppDbContext>().AsSelf().InstancePerLifetimeScope();

            //Repository
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();
            builder.RegisterType<CartItemRepository>().As<ICartItemRepository>().InstancePerLifetimeScope();
            builder.RegisterType<OrderRepository>().As<IOrderRepository>().InstancePerLifetimeScope();
            builder.RegisterType<CheckoutRepository>().As<ICheckoutRepository>().InstancePerLifetimeScope();

            //Validation
            builder.RegisterGeneric(typeof(ValidationBehavior<,>)).As(typeof(IPipelineBehavior<,>)).InstancePerDependency();

            //UserValidation
            builder.RegisterType<UserCommandValidator.AddUserCommandValidator>().As<IValidator<AddUserCommand>>().InstancePerDependency();
            builder.RegisterType<UserCommandValidator.UpdateUserCommandValidator>().As<IValidator<UpdateUserCommand>>().InstancePerDependency();

            //CartItemValidation
            builder.RegisterType<CartItemCommandValidator.AddCartItemCommandValidator>().As<IValidator<AddCartItemCommand>>().InstancePerDependency();
            builder.RegisterType<CartItemCommandValidator.UpdateCartItemCommandValidator>().As<IValidator<UpdateCartItemCommand>>().InstancePerDependency();
            builder.RegisterType<CartItemCommandValidator.DeleteCartItemCommandValidator>().As<IValidator<DeleteCartItemCommand>>().InstancePerDependency();

            //OrderValidation
            builder.RegisterType<OrderCommandValidator.DeleteOrderCommandValidator>().As<IValidator<DeleteOrderCommand>>().InstancePerDependency();
            
            //CheckoutValidation
            builder.RegisterType<CheckoutCommandValidator.CheckoutOrderCommandValidator>().As<IValidator<CheckoutOrderCommand>>().InstancePerDependency();

            //AutoMapper
            builder.Register(c => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new UserProfile());
                cfg.AddProfile(new CartItemProfile());
                cfg.AddProfile(new OrderProfile());
                cfg.AddProfile(new CheckoutProfile());
            })).AsSelf().SingleInstance();

            builder.Register(c => c.Resolve<MapperConfiguration>().CreateMapper(c.Resolve))
                .As<IMapper>()
                .InstancePerLifetimeScope();

            //Mediatr
            builder.RegisterAssemblyTypes(typeof(_ForCQRSAssemblyLoadOnly).Assembly).AsClosedTypesOf(typeof(IRequestHandler<,>));
            builder.RegisterAssemblyTypes(typeof(_ForCQRSAssemblyLoadOnly).Assembly).AsClosedTypesOf(typeof(INotificationHandler<>));
            builder.RegisterType<Mediator>().As<IMediator>().InstancePerLifetimeScope();

        }
    }
}

