using FCG.Domain.Interfaces;

namespace FCG.Application.UseCases.Users.GetUserByEmail;
public class GetUserByEmailHandler
{
    private readonly IUserRepository _userRepository;

    public GetUserByEmailHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<GetUserByEmailResponse> HandleGetUserByEmailAsync(GetUserByEmailRequest request)
    {
        var user = await _userRepository.GetUserByEmailAsync(request.Email);
        if (user is null)
        {
            throw new InvalidOperationException("User not found.");
        }
        return new GetUserByEmailResponse(user.Id, user.Name, user.Email.Address, user.Profile.ToString());
    }
}

