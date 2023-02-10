using AutoMapper;
using webapi.Domain.Models;
using webapi.Infrastructure.Database.Contexts;
using webapi.Domain.Contracts;
using webapi.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Azure.Core;

namespace webapi.Infrastructure.Database.Repository
{
    public class CheckoutRepository : ICheckoutRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<CheckoutRepository> _logger;

        public CheckoutRepository(AppDbContext context, IMapper mapper, ILogger<CheckoutRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }
        // cf43dac0-c719-420b-48b3-08db04c62ba8 - admin

        public async Task<Guid> CheckoutOrder(CheckoutModel checkout)
        {

            _logger.LogInformation("CheckoutOrder  executing..");
            var entity = _mapper.Map<Entities.Checkout>(checkout);
            var order = _context.Orders.Where(r => r.UserPrimaryID == checkout.CustomerId && r.Status == 0).FirstOrDefault();

            if(order != null && order.Status == 0)
            {
                _logger.LogInformation("Status match?", order ," ", order.Status);
                var checkoutorder = new Checkout()
                {
                    OrderPrimaryId = order.OrderId,
                    CustomerId = order.CustomerId,
                    Status = (short)checkout.Status,
                }; _context.Checkouts.Add(checkoutorder);
                _logger.LogInformation("CheckoutOrder success ..");



                order.Status = (short)checkout.Status;
                _context.Orders.Update(order);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Status update success..");

                return entity.CheckoutId;

            }

            throw new Exception("Error 400");

        }

        //public async Task<Guid> CheckoutOrder(CheckoutModel checkout)
        //{


        //    var entity = _mapper.Map<Entities.Checkout>(checkout);
        //    var order = _context.Orders.FirstOrDefault(r => r.OrderId == checkout.OrderPrimaryId);

        //    var checkoutorder = new Checkout()
        //    {
        //        OrderPrimaryId = checkout.OrderPrimaryId,
        //        CustomerId = order.CustomerId,
        //        Status = (short)checkout.Status,
        //    }; _context.Checkouts.Add(checkoutorder);



        //    order.Status = (short)checkout.Status;
        //    _context.Orders.Update(order);
        //    await _context.SaveChangesAsync();

        //    return entity.CheckoutId;


        //}
    }

}
