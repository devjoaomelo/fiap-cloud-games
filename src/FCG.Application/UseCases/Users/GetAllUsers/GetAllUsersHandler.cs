using FCG.Domain.Interfaces;

namespace FCG.Application.UseCases.Users.GetAllUsers;
public class GetAllUsersHandler
{
    private readonly IUserRepository _userRepository;

    public GetAllUsersHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<GetAllUsersResponse>> HandleGetAllUsersAsync(GetAllUsersRequest request)
    {
        var users = await _userRepository.GetAllAsync();
        return users.Select(user => new GetAllUsersResponse(user.Id, user.Name, user.Email.Address, user.Profile.ToString()));
    }
}

