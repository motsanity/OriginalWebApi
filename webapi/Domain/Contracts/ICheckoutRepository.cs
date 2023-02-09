using webapi.Domain.Models; //should be model and not entity
using webapi.Infrastructure.Database.Entities;

namespace webapi.Domain.Contracts
{
    public interface ICheckoutRepository    //added 4:41PM 1/24/2023
    {
        Task<Guid> CheckoutOrder(CheckoutModel checkout);
        
    }
}