using System.ComponentModel.DataAnnotations.Schema;
using webapi.Infrastructure.Database.Entities;

namespace webapi.Domain.Models
{
    public class CartItemModel //Models from Domain
    {
        //public CartItemModel(string cartitemname, Guid customerid, Guid orderid)
        //{

        //    CartItemName = cartitemname;
        //    CustomerId = customerid;
        //    OrderId = orderid;
        //}

        public string CartItemName { get; set; }
        public Guid CustomerId { get; set; }
        public Guid OrderPrimaryId { get; set; }




    }


}
