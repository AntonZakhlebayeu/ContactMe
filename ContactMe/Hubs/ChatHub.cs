using ContactMe.Controllers;
using ContactMe.Data;
using ContactMe.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace ContactMe.Hubs;

public class ChatHub : Hub
{

    private readonly IServiceProvider _sp;

    public ChatHub(IServiceProvider sp)
    {
        _sp = sp;
    }


    [Authorize]
    public async Task SendMessage(string theme, string message, string achiever)
    {
        var userName = Context.User!.Identity!.Name;
        var messageTime = DateTime.Now.ToString("t");
        
        var newMessage = new Message(achiever, theme, message, userName, messageTime);

        int id;

        using (var scope = _sp.CreateScope())
        {
            var _dbContext = scope.ServiceProvider.GetRequiredService<MessageDbContext>();

            _dbContext.Messages!.Add(newMessage);
            _dbContext.SaveChanges();
            
            var savedMessage = _dbContext.Messages.FirstOrDefault(m => m.Text == message);
            id = savedMessage!.Id;
        }
        await Clients.User(achiever).SendAsync("ReceiveMessage", userName, theme, message, messageTime, id);
    }


}