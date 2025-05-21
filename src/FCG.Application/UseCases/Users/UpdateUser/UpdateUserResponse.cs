namespace FCG.Application.UseCases.Users.UpdateUser
{
    public class UpdateUserResponse
    {
        public string Message { get; set; }

        public UpdateUserResponse(string message)
        {
            Message = message;
        }
    }
}
