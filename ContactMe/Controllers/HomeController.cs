using System.Diagnostics;
using ContactMe.Data;
using Microsoft.AspNetCore.Mvc;
using ContactMe.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace ContactMe.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly UserManager<User> _userManager;
    private readonly ApplicationDbContext _db;
    private readonly MessageDbContext _messageDbContext;

    public HomeController(ILogger<HomeController> logger, UserManager<User> userManager, ApplicationDbContext context, MessageDbContext messageDbContext)
    {
        _logger = logger;
        _userManager = userManager;
        _db = context;
        _messageDbContext = messageDbContext;
    }
    
    [Authorize]
    public IActionResult Index()
    {
        var user = _db.Users.FirstOrDefault(user => user.UserName == User.Identity!.Name);

        return View(user);
    }
    
    [Authorize]
    public IActionResult Mailbox()
    {
        var user = _db.Users.FirstOrDefault(user => user.UserName == User.Identity!.Name);

        return View(user);
    }
    
    [HttpPost]
    [Authorize]
    public IActionResult Mails()
    {
        ICollection<Message> messages = new List<Message>();
        var allMessages = _messageDbContext.Messages;
        
        foreach (var message in allMessages)
        {
            if(message.Achiever == User.Identity.Name)
                messages.Add(message);
        }
        
        return View(messages);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}