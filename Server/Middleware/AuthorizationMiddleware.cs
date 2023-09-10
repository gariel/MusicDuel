using Server.Common;

namespace Server.Middleware;

public class AuthorizationMiddleware : IMiddleware
{
    private readonly IUserInfo _userInfo;

    public AuthorizationMiddleware(IUserInfo userInfo)
    {
        _userInfo = userInfo;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var auth = context.Request.Headers["authorization"].FirstOrDefault() ?? "";

        //TODO: magic

        if (auth.ToLower().StartsWith("bearer "))
            auth = auth[7..];

        if (string.IsNullOrWhiteSpace(auth) && !context.Request.Path.StartsWithSegments("/login"))
            throw new Exception("user must be logged in");

        _userInfo.Email = auth;

        await next(context);
    }
}