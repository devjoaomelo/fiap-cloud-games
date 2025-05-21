namespace FCG.Application.UseCases.Users.DeleteUser
{
    public class DeleteUserResponse
    {
        public bool IsDeleted { get; set; }
        public string Message { get; set; }

        public DeleteUserResponse(bool isDeleted, string message)
        {
            IsDeleted = isDeleted;
            Message = message;
        }
    }
}
