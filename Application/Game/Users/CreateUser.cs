using Application.Common.Interfaces;
using MediatR;

namespace Application.Game.Users;

public class CreateUserRequest : IRequest<Unit>
{
    public string UserName { get; set; }
    public string Password { get; set; }
    
    public class CreateUserRequestHandlert : IRequestHandler<CreateUserRequest, Unit>
    {
        private readonly IUsers _users;

        public CreateUserRequestHandlert(IUsers users)
        {
            _users = users;
        }

        public async Task<Unit> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            await _users.CreateUser(request.UserName, request.Password);
            return Unit.Value;
        }
    }
}