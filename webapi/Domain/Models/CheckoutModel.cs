using System.ComponentModel.DataAnnotations.Schema;
using webapi.Domain.Enumerations;
using webapi.Infrastructure.Database.Entities;

namespace webapi.Domain.Models
{
    public class CheckoutModel
    {
        public Guid CheckoutId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid OrderPrimaryId { get; set; }
        public Status Status { get; set; }
    }
}
