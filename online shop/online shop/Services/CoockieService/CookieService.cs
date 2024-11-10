namespace online_shop.Services;

public class CookieService : ICookieService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CookieService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public void SetCookie(string name, string token)
    {
        var options = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict
        };


        options.IsEssential = true;

        _httpContextAccessor.HttpContext.Response.Cookies.Append(name, token, options);
    }

    public string GetCookie(string name)
    {
        _httpContextAccessor.HttpContext.Request.Cookies.TryGetValue(name, out var value);
        return value;
    }

    public void RemoveCookie(string name)
    {
        _httpContextAccessor.HttpContext.Response.Cookies.Delete(name);
    }

    public void UpdateCookie(string name, string newToken)
    {
        RemoveCookie(name);
        SetCookie(name, newToken);
    }
}