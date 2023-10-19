using AutoMapper;
using CinemaSystem.Application.DTO.Auth;
using CinemaSystem.Core.Entities;

namespace CinemaSystem.Application.MappingProfiles
{
    internal class RoleClaimMappingProfile : Profile
    {
        public RoleClaimMappingProfile()
        {
            CreateMap<RoleClaim, ClaimDto>()
                 .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id.Value))
                 .ForMember(dest => dest.ClaimName, opts => opts.MapFrom(src => src.ClaimName.Value));
        }
    }
}