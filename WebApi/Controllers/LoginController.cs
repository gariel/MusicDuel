using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{
    [HttpPost]
    public LoginToken Login([FromBody] LoginInformation info)
    {
        if (string.IsNullOrWhiteSpace(info.UserName))
            throw new Exception("empty username");
        
        if (string.IsNullOrWhiteSpace(info.Password))
            throw new Exception("empty password");
        
        return new() { Token = info.UserName };
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