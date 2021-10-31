using AutoMapper;
using CQRS.Application;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcServer.Services
{
    public class UsersService : UserService.UserServiceBase
    {
        private readonly IMapper _mapper;
        private readonly ISystemAppService _systemAppService;
        public UsersService(IMapper mapper, ISystemAppService systemAppService)
        {
            _mapper = mapper;
            _systemAppService = systemAppService;
        }
        public override async Task<GetFavoritesResponse> GetFavorites(GetFavoritesRequest request, ServerCallContext context)
        {
            var dbTopTenProducts = await _systemAppService.GetFavoritesProducts();

            GetFavoritesResponse response = new GetFavoritesResponse();
            response.Products.AddRange(_mapper.Map<List<Product>>(dbTopTenProducts));

            return response;
        }
    }
}