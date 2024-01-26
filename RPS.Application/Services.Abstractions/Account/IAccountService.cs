using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RPS.Application.Dto.Account;
using RPS.Application.Dto.Authentication.Login;
using RPS.Application.Dto.Authentication.Register;

namespace RPS.Application.Services.Abstractions.Account;

public interface IAccountService
{
    public Task<LoginResponseDto> Login(LoginRequestDto model, ModelStateDictionary modelState);

    public Task<RegisterResponseDto> Register(RegisterRequestDto model, ModelStateDictionary modelState);
    public Task<EditUserResponseDto> EditAccount(IdentityUser currentUser, EditUserRequestDto model, ModelStateDictionary modelState);
}