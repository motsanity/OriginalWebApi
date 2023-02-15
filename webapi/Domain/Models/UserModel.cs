using webapi.Infrastructure.Database.Entities;

namespace webapi.Domain.Models
{
    public class UserModel //Models from Domain
    {
        public UserModel(string? userName) //for AddUserCommand added 2/14/2023
        {
            UserName = userName;
        }

        public UserModel(string? userName, ICollection<Order> orders, Guid userid) //for any added 2/14/2023
        {
            UserName = userName;
            Orders = orders;
            UserId = userid;
        }

        //modified to private set 2/14/2023
        public string UserName { get; private set; }

        //added 02/08/2023
        public ICollection<Order> Orders { get; private set; } = new List<Order>();
        public Guid UserId { get; private set; }

    }


}
