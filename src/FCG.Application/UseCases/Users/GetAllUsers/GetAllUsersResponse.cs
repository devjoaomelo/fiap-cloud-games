namespace FCG.Application.UseCases.Users.GetAllUsers
{
    public class GetAllUsersResponse
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Email { get; }
        public string Profile { get; }

        public GetAllUsersResponse(Guid id, string name, string email, string profile)
        {
            Id = id;
            Name = name;
            Email = email;
            Profile = profile;
        }
    }
}
