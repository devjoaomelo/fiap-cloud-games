namespace FCG.Application.UseCases.Users.UpdateUser
{
    public class UpdateUserRequest
    {
        public Guid Id { get; init; }
        public string NewName { get; init; }
        public string NewPassword { get; init; }

        public UpdateUserRequest(Guid id, string newName, string newPassword)
        {
            Id = id;
            NewName = newName;
            NewPassword = newPassword;
        }
    }
}
