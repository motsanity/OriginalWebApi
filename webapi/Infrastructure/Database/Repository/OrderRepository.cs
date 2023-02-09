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

        public OrderRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderModel>> GetAllOrderByStatus(short status)
        {
            var query = "SELECT * FROM Orders WHERE Status = @status";

            using (var connection = _context.Database.GetDbConnection())
            {
                var orders = await connection.QueryAsync<OrderModel>(query, new { status });
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
                return orders.ToList();
            }
        }

        public async Task<IEnumerable<OrderModel>> GetAllOrders()
        { 
            var query = "SELECT * FROM Orders";

            using (var connection = _context.Database.GetDbConnection())
            {
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

                return orders.ToList();
            }
        }
        public async Task<OrderModel> GetOrderById(Guid orderId)
        {

            var query = "SELECT * FROM Orders WHERE OrderId = @orderId";
            using (var connection = _context.Database.GetDbConnection())
            {
                var orders = await connection.QueryAsync<OrderModel>(query, new {orderId});
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

                return orders.FirstOrDefault(o => o.OrderId == orderId);
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
