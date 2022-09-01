using System.Collections.Generic;

namespace EProductManagement.Domain.Services
{
    public interface IRequestHandler
    {
        string CreatePostRequest(Dictionary<string, string> parameters);
        string CreateGetRequest(Dictionary<string, string> parameters);
        string SendGetRequest(string Url);
        string SendPostRequest(string Request, string Url);
    }
}
