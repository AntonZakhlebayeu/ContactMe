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
    public static HomeController Instance { get; private set; }

    private readonly ILogger<HomeController> _logger;
    private readonly UserManager<User> _userManager;
    private readonly ApplicationDbContext _db;

    public HomeController(ILogger<HomeController> logger, UserManager<User> userManager, ApplicationDbContext context)
    {
        Instance = this;
        _logger = logger;
        _userManager = userManager;
        _db = context;
    }
    
    [Authorize]
    public IActionResult Index()
    {
        var user = _db.Users.FirstOrDefault(user => user.UserName == User.Identity!.Name);

        return View(user);
    }
    
    [Authorize]
    public IActionResult Chat()
    {
        var user = _db.Users.FirstOrDefault(user => user.UserName == User.Identity!.Name);

        return View(user);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}