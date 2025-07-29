using FCG.Domain.Interfaces;

namespace FCG.Application.UseCases.Users.GetUserByEmail;
public class GetUserByEmailHandler
{

    private readonly IUserValidationService _userValidationService;

    public GetUserByEmailHandler(IUserValidationService userValidationService)
    {
        _userValidationService = userValidationService;
    }

    public async Task<GetUserByEmailResponse> HandleGetUserByEmailAsync(GetUserByEmailRequest request)
    {
        var user = await _userValidationService.GetUserByEmailAsync(request.Email);

        return new GetUserByEmailResponse(user.Id, user.Name, user.Email.Address, user.Profile.ToString());
    }
}

