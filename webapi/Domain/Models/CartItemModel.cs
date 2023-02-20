using System.ComponentModel.DataAnnotations.Schema;
using webapi.Infrastructure.Database.Entities;

namespace webapi.Domain.Models
{
    public class CartItemModel //Models from Domain
    {
        
      

        //modified to private set
        public string CartItemName { get;  set; }
        public Guid CustomerId { get;  set; }
        public Guid OrderPrimaryId { get;  set; }



    }


}
