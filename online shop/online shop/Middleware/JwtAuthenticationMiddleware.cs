using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using online_shop.Repositories;

namespace online_shop.Middleware;

public class JwtAuthenticationMiddleware
{
    private readonly RequestDelegate _next;
    
    
    public JwtAuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var token = context.Request.Cookies["Access-cookie"];
        if (!string.IsNullOrEmpty(token))
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadJwtToken(token);
                
                var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
                var phone = jwtToken.Claims.FirstOrDefault(c => c.Type == "phone")?.Value;
                var claims = new List<Claim>();
            
                if (userId != null)
                {
                    claims.Add(new Claim("userId", userId));
                }

                if (phone != null)
                {
                    claims.Add(new Claim("phone", phone));
                }

                if (claims.Any()) 
                {
                    var identity = new ClaimsIdentity(claims);
                    var principal = new ClaimsPrincipal(identity);
                    context.User = principal;
                    
                }
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