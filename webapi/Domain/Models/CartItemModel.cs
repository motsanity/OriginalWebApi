using System.ComponentModel.DataAnnotations.Schema;
using webapi.Infrastructure.Database.Entities;

namespace webapi.Domain.Models
{
    public class CartItemModel //Models from Domain
    {
        
        public CartItemModel(string? cartItemName, Guid customerId, Guid orderPrimaryId) //added 2/14/2023 for AddCartItemCommand
        {
            CartItemName = cartItemName;
            CustomerId = customerId;
            OrderPrimaryId = orderPrimaryId;
        } 

        //modified to private set
        public string CartItemName { get; private set; }
        public Guid CustomerId { get; private set; }
        public Guid OrderPrimaryId { get; private set; }



    }


}
