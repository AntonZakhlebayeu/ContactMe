using Microsoft.AspNetCore.Identity;

namespace ContactMe.Models;

public sealed class User : IdentityUser
{
    public string? FirstName { get; init; }
    public string? LastName { get; set; }
    public string? Password { get; set; }
    public int Age { get; set; }

    public static ICollection<Message>? Messages { get; set; }
}