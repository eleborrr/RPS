using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RPS.Domain.Entities;

namespace RPS.Infrastructure.Database;

public sealed class ApplicationDbContext: IdentityDbContext<User>
{
    public DbSet<Message> Messages { get; set; }
    public DbSet<GameRoom> GameRooms { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    { 
        // Database.Migrate();
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>()
            .Ignore(u => u.PhoneNumber)
            .Ignore(u => u.PhoneNumberConfirmed)
            .Ignore(u => u.Email)
            .Ignore(u => u.EmailConfirmed);
        
        builder.Entity<User>().HasData(
            new User
            {
                Id = "1",
                UserName = "Glebster",
                NormalizedUserName = "GLEBSTER",
            }
        );
        
        builder.Entity<User>().HasData(
            new User
            {
                Id = "2",
                UserName = "RafLox",
                NormalizedUserName = "RAFLOX",
            }
        );
        
        base.OnModelCreating(builder);
    }
}