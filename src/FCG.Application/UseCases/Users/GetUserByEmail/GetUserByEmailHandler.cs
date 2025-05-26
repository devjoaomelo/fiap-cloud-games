using FCG.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.Application.UseCases.Users.GetUserByEmail
{
    public class GetUserByEmailHandler
    {
        private readonly IUserRepository _userRepository;

        public GetUserByEmailHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<GetUserByEmailResponse> HandleGetUserByEmailAsync(GetUserByEmailRequest request)
        {
            var user = await _userRepository.GetUserByEmailAsync(request.Email);
            if (user == null)
            {
                throw new InvalidOperationException("User not found.");
            }
            return new GetUserByEmailResponse(user.Id, user.Name, user.Email.Address, user.Profile);
        }
    }
}
