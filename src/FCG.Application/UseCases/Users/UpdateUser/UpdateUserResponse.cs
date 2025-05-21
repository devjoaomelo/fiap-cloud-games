namespace FCG.Application.UseCases.Users.UpdateUser
{
    public class UpdateUserResponse
    {
        public string Message { get; init; }

        public UpdateUserResponse(string message)
        {
            Message = message;
        }
    }
}
