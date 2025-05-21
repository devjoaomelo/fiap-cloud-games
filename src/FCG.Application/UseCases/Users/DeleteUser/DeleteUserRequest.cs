namespace FCG.Application.UseCases.Users.DeleteUser
{
    public class DeleteUserRequest
    {
        public Guid Id { get; init; }

        public DeleteUserRequest(Guid id)
        {
            Id = id;
        }
    }
}
