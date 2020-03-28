using Microsoft.AspNetCore.Http;

namespace DatingApp.API.handlers
{
    public static class Extensions
    {
        public static void ApplicationError(this HttpResponse response, string  messege)
        {
            response.Headers.Add("Application-Error",messege);
            response.Headers.Add("Access-Control-Expose-Headers","Application-Error");
            response.Headers.Add("Access-Control-Allow-Origin","*");
        }
    }
}