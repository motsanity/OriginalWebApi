using System.ComponentModel.DataAnnotations.Schema;
using webapi.Infrastructure.Database.Entities;

namespace webapi.CQRS.ViewModels
{
    public class CartItemViewModel
    {
        
        public string CartItemName { get; set; }
        public Guid CustomerId { get; set; }


    }
}
