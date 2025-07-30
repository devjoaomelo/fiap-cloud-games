using System.ComponentModel.DataAnnotations;

namespace FCG.Application.UseCases.Users.UpdateUser;
public class UpdateUserRequest
{
    public Guid Id { get; set; }
    [Required]
    public string NewName { get; set; }
    [Required]
    public string NewPassword { get; set; }

    public UpdateUserRequest(Guid id, string newName, string newPassword)
    {
        Id = id;
        NewName = newName;
        NewPassword = newPassword;
    }
}

