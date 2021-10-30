using AutoMapper;
using CQRS.Core.Entities;
using CQRS.Core.Interfaces.QueryInterfaces;
using CQRS.Domain.Dtos.UserDtos;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Domain.Queries.UserQueries
{
    public class UserQueryHandler : IRequestHandler<GetUsersQuery, List<UserDto>>,
                                      IRequestHandler<GetUserDetailQuery, UserDto>,
                                      IRequestHandler<AuthenticateQuery, UserDto>
    {
        private readonly IQueryUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        public UserQueryHandler(IQueryUserRepository userRepository, UserManager<User> userManager, IMapper mapper)
        {
            _userRepository = userRepository;
            _userManager = userManager;
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
        public async Task<UserDto> Handle(AuthenticateQuery request, CancellationToken cancellationToken)
        {
            var dbUser = await _userManager.FindByEmailAsync(request.Email.Trim());
            if (dbUser == null || !(await _userManager.CheckPasswordAsync(dbUser, request.Password.Trim())))
            {
                throw new ApplicationException("Invalid credentials");
            }
            return _mapper.Map<UserDto>(dbUser);
        }
    }
}
