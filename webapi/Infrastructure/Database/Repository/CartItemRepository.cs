using Microsoft.EntityFrameworkCore;
using Dapper;
using AutoMapper;
using webapi.Domain.Models;
using webapi.Infrastructure.Database.Contexts;
using webapi.Domain.Contracts;
using System.Drawing;
using webapi.Infrastructure.Database.Entities;

namespace webapi.Infrastructure.Database.Repository
{
    public class CartItemRepository : ICartItemRepository //added 4:45PM 1/24/2023
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<CartItemRepository> _logger;

        public CartItemRepository(AppDbContext context, IMapper mapper, ILogger<CartItemRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }



        public async Task<Guid> AddCartItem(CartItemModel cartitem)

        {
            _logger.LogInformation("AddCartItem executing..");

            var user = _context.Users.FirstOrDefault(r => r.UserId == cartitem.CustomerId);
            var userOrder = _context.Orders.FirstOrDefault(r => r.UserPrimaryID == cartitem.CustomerId && r.Status == 0);
            var entity = _mapper.Map<Entities.CartItem>(cartitem);
            short status = 0;

            //_context.CartItems.Add(entity);



            if (userOrder != null && userOrder.Status == 0)
            {
                _logger.LogInformation("search match?", userOrder);

                entity.OrderPrimaryId = userOrder.OrderId;
                userOrder.CartItems.Add(entity);

                _logger.LogInformation("Update status success..");
                _context.Orders.Update(userOrder);
                

            }
            else
            {
                _logger.LogInformation("Create new Order");
                var order = new Order()
                {
                    CustomerId = user.UserId,
                    UserPrimaryID = user.UserId,
                    Status = status,
                    CartItems = new List<CartItem>() { entity }

                };

                _logger.LogInformation("Save Order success..");
                _context.Orders.Add(order);

            }

            await _context.SaveChangesAsync();

            return entity.CartItemId;

        }

        public async Task<List<CartItemModel>> GetAllCartItems()
        {

            var query = @"
                SELECT ci.* 
                FROM CartItems ci 
                INNER JOIN Orders o 
                ON ci.OrderPrimaryId = o.OrderId 
                WHERE o.Status = 0
            ";

            using (var connection = _context.Database.GetDbConnection())
            {
                _logger.LogInformation("GetAllCartItem executing..");
                try
                {
                    var cartitems = await connection.QueryAsync<CartItemModel>(query);
                    _logger.LogInformation("Success..");
                    return cartitems.ToList();
                    

                }

                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error in GetAllCartItem");
                    throw new Exception("Error in retrieving CartItem", ex);
                }
            }

            //var query = "SELECT * FROM CartItems";

            //using(var connection = _context.Database.GetDbConnection())
            //{
            //    var cartitems = await connection.QueryAsync<CartItemModel>(query);
            //    return cartitems.ToList();
            //}
        }

        public async Task UpdateCartItem(Guid CartItemId, string cartitemname) 
        {
            _logger.LogInformation("UpdateCartItem executing..");
            try
            {
                
                var entity = await FindCartItemById(CartItemId);
                entity.CartItemName = cartitemname;
                await _context.SaveChangesAsync();
                _logger.LogInformation("Update success..");

            }


            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Update CartItem");
                throw new Exception("Error: cant find the desired user", ex);
            }

        }

        public async Task DeleteCartItem(Guid CartItemId)
        {
            _logger.LogInformation("DeleteCartItem executing..");
            try
            {
                var entity = await FindCartItemById(CartItemId);
                _context.CartItems.Remove(entity);
                await _context.SaveChangesAsync();
                _logger.LogInformation("DeleteCartItem success..");
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Delete CartItem");
                throw new Exception("Error: cant find the desired user", ex);
            }


        }


        //Private section
        private async Task<Entities.CartItem> FindCartItemById(Guid id) //added 4:55PM 1/24/2023
        {
            var found = await _context.CartItems.FindAsync(id);
            if (found == null)
                throw new NullReferenceException();

            return found;
        }




    }
}
