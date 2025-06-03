using System.ComponentModel.DataAnnotations;

namespace FCG.Application.UseCases.Users.UpdateUser;
public class UpdateUserRequest
{
    public Guid Id { get; init; }
    [Required]
    public string NewName { get; init; }
    [Required]
    public string NewPassword { get; init; }

    public UpdateUserRequest(Guid id, string newName, string newPassword)
    {
        Id = id;
        NewName = newName;
        NewPassword = newPassword;
    }
}

