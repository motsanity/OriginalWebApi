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
        private readonly ILogger<UserRepository> _logger;
        public UserRepository(AppDbContext context, IMapper mapper, ILogger<UserRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;

        }

        public async Task<Guid> AddUser(UserModel user)
        {

            _logger.LogInformation("Adding User executing..");
            try
            {
                var entity = _mapper.Map<Entities.User>(user);
                _context.Users.Add(entity);
                await _context.SaveChangesAsync();

                _logger.LogInformation("User added successfully: {@user}", user);

                return entity.UserId;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding user: {@user}", user);
                return Guid.Empty;
            }

        }

        public async Task<UserModel> GetUserById(Guid userId)
        {
            _logger.LogInformation("Get User By Id executing..");

            try
            {
                var query = "SELECT * FROM Users WHERE UserId = @userId";

                using (var connection = _context.Database.GetDbConnection())
                {
                    _logger.LogInformation("Connection established..");

                    var user = await connection.QuerySingleOrDefaultAsync<UserModel>(query, new { userId });

                    _logger.LogInformation("Fetch successfully..");

                    return user;

                }

            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetUserById");
                throw new Exception("Error in retrieving user by Id", ex);
            }

        }

        public async Task UpdateUser(Guid UserId, string username) //added 1:28PM 1/24/2023
        {
            _logger.LogInformation("Update User executing..");
            try
            {
                var entity = await FindUserById(UserId);
                entity.UserName = username;
                _logger.LogInformation("Update User Sucess..");
                await _context.SaveChangesAsync();

            }

            catch(Exception ex)
            {
                _logger.LogError(ex, "Error updating username");
                throw new Exception("Error status", ex);
            } 

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
