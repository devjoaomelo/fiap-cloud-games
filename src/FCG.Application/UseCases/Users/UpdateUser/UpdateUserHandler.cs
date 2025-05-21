using FCG.Domain.Interfaces;
using FCG.Domain.ValueObjects;

namespace FCG.Application.UseCases.Users.UpdateUser
{
    public class UpdateUserHandler
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UpdateUserResponse> HandleUpdateUserAsync(UpdateUserRequest request)
        {
            var user = await _userRepository.GetUserByIdAsync(request.Id);

            if (user == null)
                return new UpdateUserResponse("User not found.");

            var newPassword = new Password(request.NewPassword);
            user.Update(request.NewName, newPassword);

            await _userRepository.UpdateUserAsync(user);
            return new UpdateUserResponse("Update complete.");
        }
    }
}
