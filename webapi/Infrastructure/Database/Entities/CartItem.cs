using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

namespace webapi.Infrastructure.Database.Entities
{
    public class CartItem       //added 3:15pm 1/24/2023
    {
        [Key]
        public Guid CartItemId { get; set; }
        public string? CartItemName { get; set; } //nullable
        public Guid CustomerId { get; set; }


        //[ForeignKey("OrderPrimaryId")]

        [ForeignKey("Order")]
        public Guid OrderPrimaryId { get; set; }
        public virtual Order Order { get; set; }
        
    }
}
