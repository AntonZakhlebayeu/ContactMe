using ContactMe.Data;
using ContactMe.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace ContactMe.Hubs;

public class ChatHub : Hub
{
    [Authorize]
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
    
}