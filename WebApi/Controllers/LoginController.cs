using Microsoft.AspNetCore.Mvc;
using WebApi.Security;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{
    private readonly IAuthenticationTokenManager _authManager;

    public LoginController(IAuthenticationTokenManager authManager)
    {
        _authManager = authManager;
    }

    [HttpPost]
    public LoginToken Login([FromBody] LoginInformation info)
    {
        if (string.IsNullOrWhiteSpace(info.UserName))
            throw new Exception("empty username");
        
        if (string.IsNullOrWhiteSpace(info.Password))
            throw new Exception("empty password");
        
        // validation logic

        var token = _authManager.BuildToken(new TokenInfo
        {
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