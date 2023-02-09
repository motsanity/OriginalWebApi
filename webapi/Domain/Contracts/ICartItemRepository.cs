using webapi.Domain.Models; //should be model and not entity
using webapi.Infrastructure.Database.Entities;

namespace webapi.Domain.Contracts
{
    public interface ICartItemRepository    //added 4:41PM 1/24/2023
    {
        Task<Guid> AddCartItem(CartItemModel cartitem);
        Task<List<CartItemModel>> GetAllCartItems();
        Task UpdateCartItem(Guid CartItemId, string? CartItemName);

        Task DeleteCartItem(Guid CartItemId);
    }
}