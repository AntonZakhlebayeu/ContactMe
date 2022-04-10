using ContactMe.Controllers;
using ContactMe.Data;
using ContactMe.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SQLitePCL;

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
        var messageTime = DateTime.UtcNow.AddHours(3).ToString("t");
        
        var newMessage = new Message(achiever, theme, message, userName, messageTime);
        
        User _achiever;

        using (var scope = _sp.CreateScope())
        {
            int id;
            
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var messageContext = scope.ServiceProvider.GetRequiredService<MessageDbContext>();

            _achiever = dbContext.Users.FirstOrDefault(u => u.Email == achiever)!;

            if (_achiever == null)
                await Clients.User(Context.UserIdentifier!).SendAsync("ErrorMessage", achiever);
            else
            {
                messageContext.Messages!.Add(newMessage);
                await messageContext.SaveChangesAsync();

                var savedMessage = messageContext.Messages.FirstOrDefault(m => m.Text == message);
                id = savedMessage!.Id;
                await Clients.User(achiever).SendAsync("ReceiveMessage", userName, theme, message, messageTime, id);
            }
        }
    }


}