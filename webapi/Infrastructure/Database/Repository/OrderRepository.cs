using Microsoft.EntityFrameworkCore;
using Dapper;
using AutoMapper;
using webapi.Domain.Models;
using webapi.Infrastructure.Database.Contexts;
using webapi.Domain.Contracts;
using System.Drawing;
using webapi.Infrastructure.Database.Entities;
using Microsoft.Data.SqlClient;
using webapi.CQRS.ViewModels;

namespace webapi.Infrastructure.Database.Repository
{
    public class OrderRepository : IOrderRepository //added 4:45PM 1/24/2023
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderRepository> _logger;

        public OrderRepository(AppDbContext context, IMapper mapper, ILogger<OrderRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<OrderModel>> GetAllOrderByStatus(short status)
        {

            _logger.LogInformation("GetAllOrderByStatus  executing..");
            var query = "SELECT * FROM Orders WHERE Status = @status";

            using (var connection = _context.Database.GetDbConnection())
            {
                try
                {
                    var orders = await connection.QueryAsync<OrderModel>(query, new { status });

                    _logger.LogInformation("Connection established..");

                    var orderIds = orders.Select(o => o.OrderId).ToArray();
                    var cartItems = await _context.CartItems
                        .Where(c => orderIds.Contains(c.OrderPrimaryId))
                        .ToListAsync();


                    foreach (var order in orders)
                    {
                        order.CartItems = cartItems
                            .Where(c => c.OrderPrimaryId == order.OrderId)
                            .ToList();
                    }
                    _logger.LogInformation("GetAllOrderByStatus  success..");
                    return orders.ToList();

                }

                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error in GetAllOrderByStatus");
                    throw new Exception("Error in retrieving all Order by Status", ex);
                }

            }
        }

        public async Task<IEnumerable<OrderModel>> GetAllOrders()
        {
            _logger.LogInformation("GetAllOrder executing..");
            var query = "SELECT * FROM Orders";

            using (var connection = _context.Database.GetDbConnection())
            {
                try
                {
                    _logger.LogInformation("Connection established..");
                    var orders = await connection.QueryAsync<OrderModel>(query);
                    var orderIds = orders.Select(o => o.OrderId).ToArray();
                    var cartItems = await _context.CartItems
                        .Where(c => orderIds.Contains(c.OrderPrimaryId))
                        .ToListAsync();

                    foreach (var order in orders)
                    {
                        order.CartItems = cartItems
                            .Where(c => c.OrderPrimaryId == order.OrderId)
                            .ToList();
                    }
                    _logger.LogInformation("GetAllOrder success..");
                    return orders.ToList();

                }

                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error in GetAllOrder");
                    throw new Exception("Error in retrieving all Order", ex);
                }

            }
        }
        public async Task<OrderModel> GetOrderById(Guid orderId)
        {
            _logger.LogInformation("GetOrderById executing..");

            var query = "SELECT * FROM Orders WHERE OrderId = @orderId";
            using (var connection = _context.Database.GetDbConnection())
            {
                try
                {
                    _logger.LogInformation("Connection established..");
                    var orders = await connection.QueryAsync<OrderModel>(query, new { orderId });
                    var orderIds = orders.Select(o => o.OrderId).ToArray();
                    var cartItems = await _context.CartItems
                        .Where(c => orderIds.Contains(c.OrderPrimaryId))
                        .ToListAsync();


                    foreach (var order in orders)
                    {
                        order.CartItems = cartItems
                            .Where(c => c.OrderPrimaryId == order.OrderId)
                            .ToList();
                    }

                    _logger.LogInformation("GetOrderById success..");
                    return orders.FirstOrDefault(o => o.OrderId == orderId);

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error in GetOrderById");
                    throw new Exception("Error in retrieving an Order", ex);
                }

            }

            //using (var connection = _context.Database.GetDbConnection())
            //{
            //    var order = await connection.QuerySingleOrDefaultAsync<OrderModel>(query, new { orderId });

            //    return order;

            //}
        }

        public async Task DeleteOrder(Guid id)
        {
            var entityOrder = await FindIdOrder(id);
            _context.Orders.Remove(entityOrder);

            //var entityCartItem = await FindIdCartItem(orderId);
            //_context.CartItems.Remove(entityCartItem);

            await _context.SaveChangesAsync();

        }


        //Private section


        private async Task<Entities.Order> FindIdOrder(Guid id) //added 4:55PM 1/24/2023
        {
            var foundOrder = await _context.Orders.FindAsync(id);
            if (foundOrder == null)
                throw new NullReferenceException();

            return foundOrder;
        }

        
    }
}
