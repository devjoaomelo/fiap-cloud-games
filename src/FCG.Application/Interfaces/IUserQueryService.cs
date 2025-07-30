using FCG.Application.UseCases.Users.GetAllUsers;
using FCG.Application.UseCases.Users.GetUserByEmail;
using FCG.Application.UseCases.Users.GetUserById;

namespace FCG.Application.Interfaces;

public interface IUserQueryService
{
    Task<IEnumerable<GetAllUsersResponse>> GetAllAsync();
    Task<GetUserByIdResponse> GetByIdAsync(Guid id);
    Task<GetUserByEmailResponse> GetByEmailAsync(string email);
    
}