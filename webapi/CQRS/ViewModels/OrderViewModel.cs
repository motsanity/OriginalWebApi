
using webapi.Domain.Models;
using webapi.Infrastructure.Database.Entities;

namespace webapi.CQRS.ViewModels
{
    public class OrderViewModel
    {
        public Guid OrderId { get; set; }
        public Guid CustomerId { get; set; }
        public string Status { get; set; } //new 9:53am 2/1/2023
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    }
}
