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
using webapi.CQRS.Validation.CommandValidation.UserCommandValidator.CommandValidator;
using System.Reflection;
using webapi.CQRS.Command.CommandCartItem;
using webapi.CQRS.Command.CommandOrder;
using webapi.CQRS.Command.CommandCheckout;

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


builder.Services.AddAutoMapper(typeof(_ForAppServiceAssemblyLoadOnly).Assembly);
builder.Services.AddMediatR(typeof(_ForCQRSAssemblyLoadOnly).Assembly);
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICartItemRepository, CartItemRepository>(); //added 1:50pm 1/24/2023
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<ICheckoutRepository, CheckoutRepository>();
builder.Services.AddDbContext<AppDbContext>(ServiceLifetime.Scoped);
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

//Validation
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

//UserValidation
builder.Services.AddTransient<IValidator<AddUserCommand>, UserCommandValidator.AddUserCommandValidator>();
builder.Services.AddTransient<IValidator<UpdateUserCommand>, UserCommandValidator.UpdateUserCommandValidator>();

//CartItemValidation
builder.Services.AddTransient<IValidator<AddCartItemCommand>, CartItemCommandValidator.AddCartItemCommandValidator>();
builder.Services.AddTransient<IValidator<UpdateCartItemCommand>, CartItemCommandValidator.UpdateCartItemCommandValidator>();
builder.Services.AddTransient<IValidator<DeleteCartItemCommand>, CartItemCommandValidator.DeleteCartItemCommandValidator>();

//OrderValidation
builder.Services.AddTransient<IValidator<DeleteOrderCommand>, OrderCommandValidator.DeleteOrderCommandValidator>();

//CheckoutValidation
builder.Services.AddTransient<IValidator<CheckoutOrderCommand>, CheckoutCommandValidator.CheckoutOrderCommandValidator>();






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




