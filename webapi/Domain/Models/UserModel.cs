using webapi.Infrastructure.Database.Entities;

namespace webapi.Domain.Models
{
    public class UserModel //Models from Domain
    {
        //public UserModel(string username)
        //{

        //    UserName = username;
        //}

        public string UserName { get; private set; }

        //added 02/08/2023
        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public Guid UserId { get; private set; }

    }


}
