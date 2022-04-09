using System.ComponentModel.DataAnnotations;

namespace ContactMe.Models;

public sealed class Message
{
    public int Id { get; set; }
    public string? Achiever { get; set; }
    [Required]
    public string? Theme { get; set; }
    [Required]
    public string? Text { get; set; }
    public string? Sender { get; set; }
    public string? Time { get; set; }

    public Message(string achiever, string theme, string text, string? sender, string time)
    {
        Achiever = achiever;
        Theme = theme;
        Text = text;
        Sender = sender;
        Time = time;
    }
}