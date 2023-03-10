using webapi.Domain.Models;

namespace webapi.Domain.Contracts
{
    public interface IOrderRepository
    {
        Task<IEnumerable<OrderModel>> GetAllOrderByStatus(short status);
        Task<IEnumerable<OrderModel>> GetAllOrders();
        Task<IEnumerable<OrderModel>> GetAllOrderByStatic();
        Task<OrderModel> GetOrderById(Guid orderId);
        Task DeleteOrder(Guid orderId);
        
    }
}
