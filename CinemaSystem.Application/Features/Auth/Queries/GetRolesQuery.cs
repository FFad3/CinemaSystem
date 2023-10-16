using AutoMapper;
using CinemaSystem.Application.Abstraction.Requests;
using CinemaSystem.Application.DTO.Auth;
using CinemaSystem.Core.Repositories.Auth;

namespace CinemaSystem.Application.Features.Auth.Queries
{
    public sealed class GetRolesQuery : IQuery<IEnumerable<RoleDto>>
    {
    }

    internal sealed class GetRolesQueryHandler : IQueryHandler<GetRolesQuery, IEnumerable<RoleDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRoleRepository _roleRepository;

        public GetRolesQueryHandler(IMapper mapper, IRoleRepository roleRepository)
        {
            _mapper = mapper;
            _roleRepository = roleRepository;
        }

        public async Task<IEnumerable<RoleDto>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = await _roleRepository.GetAllAsync(cancellationToken);

            var result = _mapper.Map<IEnumerable<RoleDto>>(roles);

            return result;
        }
    }
}