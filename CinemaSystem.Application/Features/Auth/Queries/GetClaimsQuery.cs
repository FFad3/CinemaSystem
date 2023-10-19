using AutoMapper;
using CinemaSystem.Application.Abstraction.Requests;
using CinemaSystem.Application.DTO.Auth;
using CinemaSystem.Core.Repositories.Auth;

namespace CinemaSystem.Application.Features.Auth.Queries
{
    public sealed record GetClaimsQuery : IQuery<IEnumerable<ClaimDto>>
    {
    }

    internal sealed class GetRoleClaimsQueryHandler : IQueryHandler<GetClaimsQuery, IEnumerable<ClaimDto>>
    {
        private readonly IClaimRepository _claimRepository;
        private readonly IMapper _mapper;

        public GetRoleClaimsQueryHandler(IClaimRepository claimRepository, IMapper mapper)
        {
            _claimRepository = claimRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClaimDto>> Handle(GetClaimsQuery request, CancellationToken cancellationToken)
        {
            var claims = await _claimRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            var result = _mapper.Map<IEnumerable<ClaimDto>>(claims);
            return result;
        }
    }
}