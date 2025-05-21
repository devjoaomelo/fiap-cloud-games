namespace FCG.Application.UseCases.Users.UpdateUser
{
    public class UpdateUserRequest
    {
        public Guid Id { get; set; }
        public string NewName { get; set; }
        public string NewPassword { get; set; }

        public UpdateUserRequest(Guid id, string newName, string newPassword)
        {
            Id = id;
            NewName = newName;
            NewPassword = newPassword;
        }
    }
}
