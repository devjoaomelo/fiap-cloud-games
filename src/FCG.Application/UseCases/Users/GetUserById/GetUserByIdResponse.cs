namespace FCG.Application.UseCases.Users.GetUserById
{
    public class GetUserByIdResponse
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Email { get; init; }
        public string Profile { get; init; }

        public GetUserByIdResponse(Guid id, string name, string email, string profile)
        {
            Id = id;
            Name = name;
            Email = email;
            Profile = profile;
        }
    }
}
