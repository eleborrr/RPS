using RPS.Application.Services.Abstractions.Account;
using RPS.Domain.Services.Abstractions.Chat;

namespace RPS.Application.Services.Abstractions;

public interface IServiceManager
{
    IChatService ChatService { get; }
    IAccountService AccountService { get; }
}