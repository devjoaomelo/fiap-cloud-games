using FCG.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.Application.UseCases.Users.GetUserByEmail
{
    public class GetUserByEmailResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Profile Profile { get; set; }

        public GetUserByEmailResponse(Guid id, string name, string email, Profile profile)
        {
            Id = id;
            Name = name;
            Email = email;
            Profile = profile;

        }
    }
}
