namespace FCG.Application.UseCases.Users.GetUserById
{
    public class GetUserByIdResponse
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Email { get; }
        public string Profile { get; }

        public GetUserByIdResponse(Guid id, string name, string email, string profile)
        {
            Id = id;
            Name = name;
            Email = email;
            Profile = profile;
        }
    }
}
