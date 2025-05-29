using FCG.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.Application.UseCases.Users.GetUserByEmail;
public class GetUserByEmailResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Email { get; init; }
    public string Profile { get; init; }

    public GetUserByEmailResponse(Guid id, string name, string email, string profile)
    {
        Id = id;
        Name = name;
        Email = email;
        Profile = profile;

    }
}

