namespace FCG.Application.UseCases.Users.GetUserByEmail
{
    public class GetUserByEmailRequest
    {
        public string Email { get; set; }
        public GetUserByEmailRequest(string email)
        {
            Email = email;
        }
    }

}
