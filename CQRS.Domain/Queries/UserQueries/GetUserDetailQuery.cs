using CQRS.Domain.Dtos.CourseDtos;
using CQRS.Domain.Dtos.UserDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Domain.Queries.UserQueries
{
    public class GetUserDetailQuery : IRequest<UserDto>
    {
        public GetUserDetailQuery(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }
}
