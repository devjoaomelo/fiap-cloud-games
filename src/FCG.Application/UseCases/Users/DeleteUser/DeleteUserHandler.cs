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

        public async Task<DeleteUserResponse> Handle(DeleteUserRequest request)
        {
            var user = await _userRepository.GetUserByIdAsync(request.Id);

            if (user == null)
                return new DeleteUserResponse(false, "User not found.");

            await _userRepository.DeleteUserAsync(request.Id);

            return new DeleteUserResponse(true, "User removed.");
        }
    }
}
