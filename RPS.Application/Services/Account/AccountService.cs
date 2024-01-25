using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using RPS.Application.Dto.Account;
using RPS.Application.Dto.Authentication.Login;
using RPS.Application.Dto.Authentication.Register;
using RPS.Application.Helpers.JwtGenerator;
using RPS.Application.Services.Abstractions.Account;
using RPS.Domain.Entities;
using ModelStateDictionary = Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary;

namespace RPS.Application.Services.Account;

public class AccountService : IAccountService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IPasswordHasher<User> _passwordHasher;

    public AccountService(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IJwtGenerator jwtGenerator, 
        IPasswordHasher<User> passwordHasher)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtGenerator = jwtGenerator;
        _passwordHasher = passwordHasher;
    }

    public async Task<LoginResponseDto> Login(LoginRequestDto model, ModelStateDictionary modelState)
    {
        /* -> bool rememberMe = false;
        /*if (Request.Form.ContainsKey("RememberMe"))
        {
            bool.TryParse(Request.Form["RememberMe"], out rememberMe);
        }#1#
        model.RememberMe = rememberMe;*/

        if (!modelState.IsValid) return new LoginResponseDto(LoginResponseStatus.Fail);
        
        var signedUser = await _signInManager.UserManager.FindByNameAsync(model.UserName);
        if (signedUser is null) return new LoginResponseDto(LoginResponseStatus.Fail);
        
        var result = await _signInManager.PasswordSignInAsync(signedUser.UserName!, model.Password, false,
            lockoutOnFailure: false);


        if (!result.Succeeded) return new LoginResponseDto(LoginResponseStatus.Fail);
        
        try
        {
            await _userManager.RemoveClaimAsync(signedUser, new Claim("Id", signedUser.Id));
        }
        catch (Exception)
        {
            // ignored
        }
                
        await _signInManager.UserManager.AddClaimAsync(signedUser, new Claim("Id", signedUser.Id));
       
        return new LoginResponseDto(LoginResponseStatus.Ok, await _jwtGenerator.GenerateJwtToken(signedUser.Id));

    }

    public async Task<RegisterResponseDto> Register(RegisterRequestDto model, ModelStateDictionary modelState)
    {
        if (!modelState.IsValid) return new RegisterResponseDto(RegisterResponseStatus.InvalidData);
        var user = new User
        {
            UserName = model.UserName
        };
            
        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
            return new RegisterResponseDto(
                RegisterResponseStatus.Fail,
                result.Errors.FirstOrDefault()!.Description);
                
        var userInDb = await _userManager.FindByNameAsync(user.UserName);
        if (userInDb is null)
            return new RegisterResponseDto(
                RegisterResponseStatus.Fail,
                "User registration error");
                
        await _userManager.AddClaimAsync(userInDb, new Claim(ClaimTypes.Role, "User"));
        return new RegisterResponseDto(RegisterResponseStatus.Ok);
    }

    public async Task<EditUserResponseDto> EditAccount(
        User currentUser,
        EditUserRequestDto model,
        ModelStateDictionary modelState)
    {
        if (!modelState.IsValid) return new EditUserResponseDto(EditUserResponseStatus.InvalidData);
        
        var passwordHash = model.Password == "" 
            ? currentUser.PasswordHash 
            : _passwordHasher.HashPassword(currentUser, model.Password);
        
        currentUser.UserName = model.UserName;
        currentUser.PasswordHash = passwordHash;
        
        var result = await _userManager.UpdateAsync(currentUser);
        
        if (!result.Succeeded)
            return new EditUserResponseDto(EditUserResponseStatus.Fail,
                result.Errors.FirstOrDefault()!.Description);
            
        return new EditUserResponseDto(EditUserResponseStatus.Ok);
    }
}