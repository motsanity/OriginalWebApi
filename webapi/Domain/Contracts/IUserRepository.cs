using webapi.Domain.Models; //should be model and not entity

namespace webapi.Domain.Contracts
{
    public interface IUserRepository
    {
        Task<Guid> AddUser(UserModel user);
        Task UpdateUser(Guid userId, string? userName); //added 1:23PM 1/24/2023
        Task<UserModel> GetUserById(Guid userId);
    }
}