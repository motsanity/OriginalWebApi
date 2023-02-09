using Microsoft.EntityFrameworkCore;
using Dapper;
using AutoMapper;
using webapi.Domain.Models;
using webapi.Infrastructure.Database.Contexts;
using webapi.Domain.Contracts;
using System.Drawing;
using webapi.Infrastructure.Database.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace webapi.Infrastructure.Database.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public UserRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Guid> AddUser(UserModel user)
        {
            var entity = _mapper.Map<Entities.User>(user);
            _context.Users.Add(entity);
            await _context.SaveChangesAsync();

            return entity.UserId;
        }

        public async Task<UserModel> GetUserById(Guid userId)
        {

            var query = "SELECT * FROM Users WHERE UserId = @userId";


            using (var connection = _context.Database.GetDbConnection())
            {
                var user = await connection.QuerySingleOrDefaultAsync<UserModel>(query, new { userId });

                return user;

            }


            //var query = "SELECT * FROM Users WHERE UserId = @userId";
            //using (var connection = _context.Database.GetDbConnection())
            //{
            //    var users = await connection.QueryAsync<UserModel>(query, new { userId });
            //    var userPrimaryId = users.Select(o => o.UserId);
            //    var orders = await _context.Orders
            //       .Where(c => userPrimaryId.Contains(c.CustomerId))
            //       .ToListAsync();
            //    var orderIds = orders.Select(o => o.OrderId).ToArray();






            //    //var orders = await connection.QueryAsync<OrderModel>(query, new { userId });
            //    //var orderIds = orders.Select(o => o.OrderId).ToArray();
            //    //var cartItems = await _context.CartItems
            //    //    .Where(c => orderIds.Contains(c.OrderPrimaryId))
            //    //    .ToListAsync();


            //    foreach (var user in users)
            //    {
            //        user.Orders = orders
            //            .Where(c => c.UserPrimaryID == user.UserId)
            //            .ToList();

            //        foreach (var order in orders)
            //        {

            //            var cartItems = await _context.CartItems
            //            .Where(c => orderIds.Contains(c.OrderPrimaryId))
            //            .ToListAsync();

            //            //order.CartItems = cartItems
            //            //.Where(c => c.OrderPrimaryId == order.OrderId)
            //            //.ToList();

            //        }
            //    }

            //    return users.FirstOrDefault(o => o.UserId == userId);
            //}


        }

        public async Task UpdateUser(Guid UserId, string username) //added 1:28PM 1/24/2023
        {
            var entity = await FindUserById(UserId);
            entity.UserName = username;
            await _context.SaveChangesAsync();

        }


        private async Task<Entities.User> FindUserById(Guid id) //added 1:37PM 1/24/2023
        {
            var found = await _context.Users.FindAsync(id);
            if (found == null)
                throw new NullReferenceException();

            return found;
        }


    }
}
