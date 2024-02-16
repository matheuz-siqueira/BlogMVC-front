namespace Web.Utils;

public class CookieHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public CookieHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor; 
    }

    public bool CookieInResponse()
    {
        var cookie = _httpContextAccessor.HttpContext?.Request.Cookies["access-token"]; 
        if(cookie is null)
            return false; 
        return true; 
    }

}
