using AutoMapper;
using CQRS.Core.Interfaces;
using CQRS.Domain.Dtos.CourseDtos;
using CQRS.Domain.Dtos.UserDtos;
using CQRS.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Domain.Queries.UserQueries
{
    public class UserQueryHandler : IRequestHandler<GetUsersQuery, List<UserDto>>,
                                      IRequestHandler<GetUserDetailQuery, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<UserDto> Handle(GetUserDetailQuery request, CancellationToken cancellationToken)
        {
            var dbUser = await _userRepository.GetByIdAsync(request.Id);
            return _mapper.Map<UserDto>(dbUser);
        }

        public async Task<List<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<UserDto>>(await _userRepository.GetAllAsync());
        }
    }
}
