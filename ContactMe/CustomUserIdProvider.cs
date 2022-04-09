using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
 
namespace ContactMe
{
    public class CustomUserIdProvider : IUserIdProvider
    {
        public virtual string GetUserId(HubConnectionContext connection)
        {
            return connection.User?.Identity.Name;
        }
    }
}