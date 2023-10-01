using Application.Common;
using WebApi.Security;

namespace WebApi.Middleware;

public class AuthorizationMiddleware : IMiddleware
{
    private readonly IUserInfo _userInfo;
    private readonly IAuthenticationTokenManager _authManager;

    public AuthorizationMiddleware(IUserInfo userInfo, IAuthenticationTokenManager authManager)
    {
        _userInfo = userInfo;
        _authManager = authManager;
    }

    public Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (!context.Request.Path.StartsWithSegments("/login"))
        {
            var auth = context.Request.Headers["authorization"].FirstOrDefault() ?? "";

            if (!auth.ToLower().StartsWith("bearer "))
                throw new Exception("Invalid token format");
            
            var token = auth[7..];
            var tokenInfo = _authManager.ValidateToken(token);

            _userInfo.Id = tokenInfo.UserId;
            _userInfo.Name = tokenInfo.UserName;
        }
        
        return next(context);
    }
}