namespace FCG.Application.UseCases.Users.DeleteUser
{
    public class DeleteUserResponse
    {
        public bool IsDeleted { get; init; }
        public string Message { get; init; }

        public DeleteUserResponse(bool isDeleted, string message)
        {
            IsDeleted = isDeleted;
            Message = message;
        }
    }
}
