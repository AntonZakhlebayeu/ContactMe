using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ContactMe.Models;

namespace ContactMe.Data;

public sealed class ApplicationDbContext : IdentityDbContext<User>
{
    public DbSet<Message> Messages;
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }
}