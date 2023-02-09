using System.ComponentModel.DataAnnotations.Schema;
using webapi.Domain.Enumerations;
using webapi.Infrastructure.Database.Entities;

namespace webapi.Domain.Models
{
    public class OrderModel
    {   
        public Guid CustomerId { get; set; }
        public Guid UserPrimaryID { get; set; }
        public Status Status { get; set; }  //new 9:50am 2/1/2023
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
        public Guid OrderId { get; internal set; }
    }
}

