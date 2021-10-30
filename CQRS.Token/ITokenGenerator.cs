using CQRS.Domain.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Token
{
    public interface ITokenGenerator
    {
        string GenerateJSONWebToken(UserDto user);
    }
}
