using ApiEquipment.Helpers;
using ApiEquipment.Interfaces;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace ApiEquipment.CustomMiddleware
{
    public class BlacklistEmployeeIdsMiddleware
    {
        private readonly RequestDelegate _next;

        public BlacklistEmployeeIdsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var jwtPayload = context.User.FindFirstValue("Id");
            //if (string.IsNullOrEmpty(jwtPayload))
            //{
            //    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            //    return;
            //}
            if (!string.IsNullOrEmpty(jwtPayload))
            {
                var employeeId = int.Parse(jwtPayload);
                using var scope = context.RequestServices.CreateScope();
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                var blacklist = await unitOfWork.GetRepo<IEmployeeRepo>().GetByBlacklistIdAsync();
                if (blacklist.Contains(employeeId))
                {
                    var response = new Respone
                    {
                        Status = false,
                        Message = "Employee ID is blacklisted."
                    };

                    // Serialize the 'response' object to a JSON string
                    var jsonResponse = JsonSerializer.Serialize(response);

                    // Write the JSON string to the response
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(jsonResponse);

                    return;


                    //context.Response.StatusCode = StatusCodes.Status403Forbidden;

                    //await context.Response.WriteAsync("Employee ID is blacklisted.");
                    //return;
                }
            }
            await _next(context);
        }
    }
}
