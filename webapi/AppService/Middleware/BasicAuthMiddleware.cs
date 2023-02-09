using Dapper;
using Microsoft.Data.SqlClient;
using webapi.Infrastructure.Database.Entities;

namespace webapi.AppService.Middleware
{
    public class BasicAuthMiddleware
    {
        private readonly RequestDelegate _next;
        
        public BasicAuthMiddleware(RequestDelegate next)
        {
            _next = next;
            
        }
        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.ContainsKey("x-admin-id"))
            {
                context.Response.StatusCode = 401;
                return;
            }

            string adminId = context.Request.Headers["x-admin-id"];
            Guid parsedAdminId = Guid.Parse(adminId); 
            
            if (!IsValidAdminId(parsedAdminId))
            {
                context.Response.StatusCode = 401;
                return;
            }



            await _next(context);
        }


        private bool IsValidAdminId(Guid adminId)
        {
            

            var query = "SELECT * FROM Admins WHERE AdminId = @adminId";
            string connectionString = "Data Source=(localdb)\\ProjectModels;Initial Catalog=WebApiDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            using (var connection = new SqlConnection(connectionString))
            {
                var admin = connection.QueryAsync<User>(query, new { adminId }).Result.FirstOrDefault();



                if (admin != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

    }
}