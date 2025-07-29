using FCG.Domain.Interfaces;

namespace FCG.Application.UseCases.Users.GetUserById;

public class GetUserByIdHandler
{
    private readonly IUserValidationService _userValidationService;
    public GetUserByIdHandler(IUserValidationService userValidationService)
    {
        _userValidationService = userValidationService;
    }

    public async Task<GetUserByIdResponse> HandleGetUserByIdAsync(GetUserByIdRequest request)
    {
        var user = await _userValidationService.GetUserIfExistsAsync(request.Id);
        
        return new GetUserByIdResponse(user.Id, user.Name, user.Email.ToString(), user.Profile.ToString());
    }
}

