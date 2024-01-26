using Microsoft.AspNetCore.Identity;
using RPS.Application.Helpers.JwtGenerator;
using RPS.Application.Services.Abstractions;
using RPS.Application.Services.Abstractions.Account;
using RPS.Application.Services.Account;
using RPS.Domain.Entities;

namespace RPS.Application.Services;

public class ServiceManager: IServiceManager
{
    private readonly Lazy<IAccountService> _accountService;

    public ServiceManager(UserManager<IdentityUser> userManager, 
        SignInManager<IdentityUser> signInManager,
        IJwtGenerator jwtGenerator,
        IPasswordHasher<IdentityUser> passwordHasher)
    {
        _accountService = new Lazy<IAccountService>(() => new AccountService(userManager, signInManager, jwtGenerator, passwordHasher));
    }

    public IAccountService AccountService => _accountService.Value;
}