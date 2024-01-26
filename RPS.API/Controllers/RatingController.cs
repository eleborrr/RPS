using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RPS.Application.Services.Abstractions;
using RPS.Domain.Entities;

namespace RPS.API.Controllers;

[ApiController]
[Route("[controller]")]
public class RatingController : Controller
{
    private readonly IServiceManager _serviceManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IMediator _mediator;

    public RatingController(
        IServiceManager serviceManager,
        SignInManager<IdentityUser> signInManager,
        IConfiguration configuration,
        HttpClient client,
        UserManager<IdentityUser> userManager, IMediator mediator)
    {
        _serviceManager = serviceManager;
        _signInManager = signInManager;
        _mediator = mediator;
    }
}