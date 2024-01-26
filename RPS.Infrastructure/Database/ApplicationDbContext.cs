using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RPS.Domain.Entities;

namespace RPS.Infrastructure.Database;

public sealed class ApplicationDbContext: IdentityDbContext<IdentityUser>
{
    public DbSet<Message> Messages { get; set; }
    public DbSet<GameRoom> GameRooms { get; set; }
    public DbSet<Move> Moves { get; set; }
    public DbSet<Round> Rounds { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    { 
        Database.Migrate();
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<IdentityUser>()
            .Ignore(u => u.PhoneNumber)
            .Ignore(u => u.PhoneNumberConfirmed)
            .Ignore(u => u.Email)
            .Ignore(u => u.EmailConfirmed);
        
        builder.Entity<IdentityUser>().HasData(
            new IdentityUser
            {
                Id = "1",
                UserName = "Glebster",
                NormalizedUserName = "GLEBSTER",
            }
        );


        builder.Entity<GameRoom>().HasData(
            new GameRoom
            {
                Id = "12",
                CreationDate = DateTime.Now,
                CreatorId = "2",
                ParticipantId = "1",
                CreatorConnected = true,
                EloDelta = 25,
                IsStarted = false,
                TimeToMove = 7
            });

        
        builder.Entity<IdentityUser>().HasData(
            new IdentityUser
            {
                Id = "2",
                UserName = "RafLox",
                NormalizedUserName = "RAFLOX",
            }
        );
        
        builder.Entity<Move>()
            .HasData(
                new Move("1", "Rock"));
        builder.Entity<Move>()
            .HasData(
                new Move("2", "Paper"));
        builder.Entity<Move>()
            .HasData(
                new Move("3", "Scissors"));
        
        base.OnModelCreating(builder);
    }
}