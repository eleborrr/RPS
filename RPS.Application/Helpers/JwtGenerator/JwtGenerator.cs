﻿using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using RPS.Domain.Entities;

namespace RPS.Application.Helpers.JwtGenerator;

public class JwtGenerator : IJwtGenerator
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<IdentityUser> _userManager;

    public JwtGenerator(IConfiguration conf, UserManager<IdentityUser> userManager)
    {
        _configuration = conf;
        _userManager = userManager;
    }

    public async Task<string?> GenerateJwtToken(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return null;
        var claims = await _userManager.GetClaimsAsync(user);
        var now = DateTime.UtcNow;
        var jwt = new JwtSecurityToken(
            issuer: _configuration["JWTTokenSettings:ISSUER"],
            audience: _configuration["JWTTokenSettings:AUDIENCE"],
            notBefore: now,
            claims: claims,
            expires: now.Add(TimeSpan.FromMinutes(int.Parse(_configuration["JWTTokenSettings:LIFETIMEINMINUTES"]!))),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(
                    Encoding.ASCII.GetBytes(_configuration["JWTTokenSettings:KEY"]!)),
                SecurityAlgorithms.HmacSha256));

        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

        return encodedJwt;
    }
}