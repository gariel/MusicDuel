using Application.Common.Interfaces;
using MediatR;

namespace Application.Game.Users;

public class ValidateUserRequest : IRequest<int?>
{
    public string UserName { get; set; }
    public string Password { get; set; }

    public class ValidateUserRequestHandler : IRequestHandler<ValidateUserRequest, int?>
    {
        private readonly IUsers _users;

        public ValidateUserRequestHandler(IUsers users)
        {
            _users = users;
        }

        public Task<int?> Handle(ValidateUserRequest request, CancellationToken cancellationToken)
        {
            return _users.ValidateUserAuthentication(request.UserName, request.Password);
        }
    }
}