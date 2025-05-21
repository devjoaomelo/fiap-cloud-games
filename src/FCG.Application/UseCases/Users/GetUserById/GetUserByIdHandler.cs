using FCG.Domain.Interfaces;

namespace FCG.Application.UseCases.Users.GetUserById
{
    public class GetUserByIdHandler
    {
        private readonly IUserRepository _userRepository;
        public GetUserByIdHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<GetUserByIdResponse> HandleUserAync(GetUserByIdRequest request)
        {
            var user = await _userRepository.GetUserByIdAsync(request.Id);

            if (user == null)
                return null;

            return new GetUserByIdResponse(user.Id, user.Name, user.Email.ToString(), user.Profile.ToString());
        }
    }
}
