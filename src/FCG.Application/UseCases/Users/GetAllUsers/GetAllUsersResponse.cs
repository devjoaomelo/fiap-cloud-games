namespace FCG.Application.UseCases.Users.GetAllUsers
{
    public class GetAllUsersResponse
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Email { get; init; }
        public string Profile { get; init; }

        public GetAllUsersResponse(Guid id, string name, string email, string profile)
        {
            Id = id;
            Name = name;
            Email = email;
            Profile = profile;
        }
    }
}
