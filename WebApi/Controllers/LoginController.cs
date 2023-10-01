using System.Security.Authentication;
using Application.Game.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Security;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IAuthenticationTokenManager _authManager;

    public LoginController(IMediator mediator, IAuthenticationTokenManager authManager)
    {
        _mediator = mediator;
        _authManager = authManager;
    }

    [HttpPost("create")]
    public async Task CreateUser([FromBody] LoginInformation info)
    {
        if (string.IsNullOrWhiteSpace(info.UserName))
            throw new Exception("empty username");
        
        if (string.IsNullOrWhiteSpace(info.Password))
            throw new Exception("empty password");
        
        await _mediator.Send(new CreateUserRequest
        {
            UserName = info.UserName,
            Password = info.Password,
        });
    }

    [HttpPost]
    public async Task<LoginToken> Login([FromBody] LoginInformation info)
    {
        if (string.IsNullOrWhiteSpace(info.UserName))
            throw new Exception("empty username");
        
        if (string.IsNullOrWhiteSpace(info.Password))
            throw new Exception("empty password");

        var userId = await _mediator.Send(new ValidateUserRequest
        {
            UserName = info.UserName,
            Password = info.Password,
        });

        if (userId is null)
            throw new AuthenticationException("User or password didn't match");

        var token = _authManager.BuildToken(new TokenInfo
        {
            UserId = userId.Value,
            UserName = info.UserName,
        });

        return new LoginToken
        {
            Token = token,
        };
    }

    public class LoginInformation
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class LoginToken
    {
        public string Token { get; set; }
    }
}