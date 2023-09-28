using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace WebApi.Security;

public class AuthenticationTokenManager : IAuthenticationTokenManager
{
    private readonly IAuthenticationInfoProvider _infoProvider;
    private readonly SymmetricSecurityKey _key;
    private readonly JwtSecurityTokenHandler _tokenHandler;

    public AuthenticationTokenManager(IAuthenticationInfoProvider infoProvider)
    {
        _infoProvider = infoProvider;
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(infoProvider.AuthenticationKey));
        _tokenHandler = new JwtSecurityTokenHandler();
    }

    public string BuildToken(TokenInfo info)
    {
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new(ClaimTypes.Upn, info.UserName),
            }),
            Expires = DateTime.UtcNow.AddSeconds(_infoProvider.AuthenticationExpirationSeconds),
            Issuer = _infoProvider.AuthenticationIssuer,
            Audience = _infoProvider.AuthenticationAudience,
            SigningCredentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256Signature),
        };

        var token = _tokenHandler.CreateToken(tokenDescriptor);
        return _tokenHandler.WriteToken(token);
    }

    public TokenInfo ValidateToken(string token)
    {
        try
        {
            _tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = _infoProvider.AuthenticationIssuer,
                ValidAudience = _infoProvider.AuthenticationAudience,
                IssuerSigningKey = _key
            }, out var validatedToken);

            var secToken = (JwtSecurityToken)validatedToken;
            var claims = secToken.Claims;
            
            return new TokenInfo
            {
                UserName = claims.First(c => c.Type == "upn").Value,
            };
        }
        catch (SecurityTokenException st)
        {
            throw new Exception("Error parsing token", st);
        }
    }
}