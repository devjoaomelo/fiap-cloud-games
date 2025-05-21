using FCG.Domain.Entities;
using FCG.Domain.Interfaces;
using FCG.Domain.ValueObjects;

namespace FCG.Application.UseCases.Users.CreateUser
{
    public class CreateUserHandler
    {
        private readonly IUserRepository _userRepository;

        public CreateUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<CreateUserResponse> HandleUserCreateAsync(CreateUserRequest request)
        {
            var email = new Email(request.Email);
            var password = new Password(request.Password);
            var user = new User(request.Name, email, password);

            await _userRepository.CreateUserAsync(user);

            return new CreateUserResponse(user.Id, user.Name, user.Email.Address);
        }
    }
}
