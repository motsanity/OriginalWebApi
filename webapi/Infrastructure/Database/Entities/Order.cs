using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Infrastructure.Database.Entities
{
    public class Order      //added 3:15pm 1/24/2023
    {
        [Key]
        public Guid OrderId { get; set; }
        public Guid CustomerId { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserPrimaryID { get; set; }
        public User User { get; set; } = null!;
        
        //new status 2:04 1/31/2023
        public short Status { get; set; }

        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();




    }
}
