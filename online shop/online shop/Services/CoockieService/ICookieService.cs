namespace online_shop.Services;

public interface ICookieService
{
    void RemoveCookie(string name);
    string GetCookie(string name);
    void SetCookie(string name, string token);
    public void UpdateCookie(string name, string newToken);
}