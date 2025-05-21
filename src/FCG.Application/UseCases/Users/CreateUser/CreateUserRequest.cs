namespace FCG.Application.UseCases.Users.CreateUser
{
    public class CreateUserRequest
    {
        public string Name { get; init; }
        public string Email { get; init; }
        public string Password { get; init; }

        public CreateUserRequest(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
        }
    }
}
