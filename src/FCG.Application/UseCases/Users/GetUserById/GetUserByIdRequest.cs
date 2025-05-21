using FCG.Infra.Repositories;

namespace FCG.Application.UseCases.Users.GetUserById
{
    public class GetUserByIdRequest
    {
        public Guid Id { get; set; }
        public GetUserByIdRequest(Guid id)
        {
            Id = id;
        }
    }
}
