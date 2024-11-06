using System.IdentityModel.Tokens.Jwt;
using online_shop.Repositories;

namespace online_shop.Middleware;

public class JwtAuthenticationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IServiceScopeFactory _scopeFactory;
    
    
    public JwtAuthenticationMiddleware(RequestDelegate next, IServiceScopeFactory scopeFactory)
    {
        _next = next;
        _scopeFactory = scopeFactory;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var token = context.Request.Cookies["Access-cookie"];
        var scope = _scopeFactory.CreateScope();
        var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
        if (!string.IsNullOrEmpty(token))
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadJwtToken(token);
                
                var phone = jwtToken.Claims.FirstOrDefault(c => c.Type == "phone")?.Value;
                
                var user = await userRepository.GetUserByPhoneAsync(phone);
                context.Items["User"] = user;

            }
            catch (System.Exception ex)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Invalid or expired token , login and try again");
                return;
            }
        }
        await _next(context);
    }
}