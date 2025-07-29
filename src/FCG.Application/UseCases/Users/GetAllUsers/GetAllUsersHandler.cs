using FCG.Domain.Interfaces;

namespace FCG.Application.UseCases.Users.GetAllUsers;
public class GetAllUsersHandler
{
    private readonly IUserValidationService _userValidationService;

    public GetAllUsersHandler(IUserValidationService userValidationService)
    {
        _userValidationService = userValidationService;
    }

    public async Task<IEnumerable<GetAllUsersResponse>> HandleGetAllUsersAsync(GetAllUsersRequest request)
    {
        var users = await _userValidationService.GetAllUsersAsync();
        
        return users.Select(user => new GetAllUsersResponse(user.Id, user.Name, user.Email.Address, user.Profile.ToString()));
    }
}

