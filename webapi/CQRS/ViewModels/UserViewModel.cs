using webapi.Infrastructure.Database.Entities;

namespace webapi.CQRS.ViewModels
{
    public class UserViewModel
    {
        public string? UserName { get; set; }

        //added 02/08/2023
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
