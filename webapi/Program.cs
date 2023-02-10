using Microsoft.EntityFrameworkCore;
using webapi.Domain.Contracts;
using webapi.CQRS;
using webapi.Infrastructure.Database.Contexts;
using MediatR;
using webapi.AppService;
using webapi.Infrastructure.Database.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using webapi.AppService.Middleware;
using Microsoft.AspNetCore.Mvc;
using webapi.CQRS.Command.CommandUser;
using MediatR.Extensions.FluentValidation.AspNetCore;
using FluentValidation;
using webapi.CQRS.Validation.CommandValidator;
using System.Reflection;
using webapi.CQRS.Command.CommandCartItem;
using webapi.CQRS.Command.CommandOrder;
using webapi.CQRS.Command.CommandCheckout;
using Autofac;
using webapi.AppService.AutoFacModule;
using Autofac.Extensions.DependencyInjection;
using Serilog;
using Autofac.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMediatR(typeof(_ForCQRSAssemblyLoadOnly).Assembly);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();


builder.Services.AddDbContextFactory<AppDbContext>(o =>
           o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")),
           ServiceLifetime.Scoped);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value!)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

//edited on 2/10/2023
//builder.Services.AddAutoMapper(typeof(_ForAppServiceAssemblyLoadOnly).Assembly);
//builder.Services.AddMediatR(typeof(_ForCQRSAssemblyLoadOnly).Assembly);
//builder.Services.AddScoped<IUserRepository, UserRepository>();
//builder.Services.AddScoped<ICartItemRepository, CartItemRepository>(); //added 1:50pm 1/24/2023
//builder.Services.AddScoped<IOrderRepository, OrderRepository>();
//builder.Services.AddScoped<ICheckoutRepository, CheckoutRepository>();
//builder.Services.AddDbContext<AppDbContext>(ServiceLifetime.Scoped);
//added

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    //added 2/9/2023
    c.SwaggerDoc("v2", new OpenApiInfo { Title = "My API", Version = "v2" });
    c.OperationFilter<AddHeaderOperationFilter>();
});

builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
});

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});


//edited on 2/10/2023
//Validation
//builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

////UserValidation
//builder.Services.AddTransient<IValidator<AddUserCommand>, UserCommandValidator.AddUserCommandValidator>();
//builder.Services.AddTransient<IValidator<UpdateUserCommand>, UserCommandValidator.UpdateUserCommandValidator>();

////CartItemValidation
//builder.Services.AddTransient<IValidator<AddCartItemCommand>, CartItemCommandValidator.AddCartItemCommandValidator>();
//builder.Services.AddTransient<IValidator<UpdateCartItemCommand>, CartItemCommandValidator.UpdateCartItemCommandValidator>();
//builder.Services.AddTransient<IValidator<DeleteCartItemCommand>, CartItemCommandValidator.DeleteCartItemCommandValidator>();

////OrderValidation
//builder.Services.AddTransient<IValidator<DeleteOrderCommand>, OrderCommandValidator.DeleteOrderCommandValidator>();

////CheckoutValidation
//builder.Services.AddTransient<IValidator<CheckoutOrderCommand>, CheckoutCommandValidator.CheckoutOrderCommandValidator>();


//serliog
var logger = new LoggerConfiguration()
    .WriteTo.File("../logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
builder.Services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));

//Autofac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>( builder => builder.RegisterModule(new AutofacModule()));
builder.Services.AddAutofac();
 



//end edited
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options => {
        options.SuppressModelStateInvalidFilter = true;
    });




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    //app.UseSwaggerUI();
    //added 2/9/2023
    app.UseApiVersioning();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
        c.SwaggerEndpoint("/swagger/v2/swagger.json", "My API v2");
    });
}
app.UseMiddleware<BasicAuthMiddleware>();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.Run();




