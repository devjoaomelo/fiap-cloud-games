using FCG.Domain.Interfaces;

namespace FCG.Application.UseCases.Users.DeleteUser
{
    public class DeleteUserHandler
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task HandleDeleteUserAsync(DeleteUserRequest request)
        {
            var user = await _userRepository.GetUserByIdAsync(request.Id);
            if (user == null)
                throw new Exception("User not found.");

            await _userRepository.DeleteUserAsync(request.Id);
        }
    }
}
