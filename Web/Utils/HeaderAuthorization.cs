using System.Net.Http.Headers;

namespace Web.Utils;

public class HeaderAuthorization
{
    public void PutTokenInHeaderAuthorization(string token, HttpClient client)
    {
        client.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue("Bearer", token);
    }
}
