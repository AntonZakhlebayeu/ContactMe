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

        /*
        if(Context.UserIdentifier!=achiever) 
            await Clients.User(Context.UserIdentifier!).SendAsync("ReceiveMessage", userName, message);
            */

        var messageTime = DateTime.Now.ToString("t");
        await Clients.User(achiever).SendAsync("ReceiveMessage", userName, theme, message, messageTime, achiever);
    }

    public void SaveMessage(string sender, string theme, string text, string achiever, string time)
    {

        Console.WriteLine(_sp);

        
        using (var scope = _sp.CreateScope())
        {
            var _dbContext = scope.ServiceProvider.GetRequiredService<MessageDbContext>();
            var message = new Message(achiever, theme, text, sender, time);
            
            _dbContext.Messages!.Add(message);
            _dbContext.SaveChanges();
        }
    }

    
}