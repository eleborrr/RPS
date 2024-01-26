using RPS.Application.Services.Abstractions.Account;

namespace RPS.Application.Services.Abstractions;

public interface IServiceManager
{
    IAccountService AccountService { get; }
}