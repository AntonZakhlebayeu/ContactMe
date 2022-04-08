using System.ComponentModel.DataAnnotations;

namespace ContactMe.Models;

public sealed class Message
{
    public int Id { get; set; }
    [Required]
    public string? UserName { get; set; }
    [Required]
    public string? Text { get; set; }
    public DateTime When { get; set; }
    
    public string UserId { get; set; }
    
    public User? Sender { get; set; }
}