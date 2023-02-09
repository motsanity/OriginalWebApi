using System.ComponentModel.DataAnnotations;

namespace webapi.Infrastructure.Database.Entities
{
    public class User //Entities
    {

        [Key]
        public Guid UserId { get; set; }
        public string? UserName { get; set; }
        public List<Order>? Orders { get; set; } = new List<Order>(); //added 3:15pm 1/24/2023

        



    }
}
