using Microsoft.AspNetCore.Identity;

namespace RPS.Domain.Entities;

public class User: IdentityUser
{
    public string Username { get; set; } = default!;
}