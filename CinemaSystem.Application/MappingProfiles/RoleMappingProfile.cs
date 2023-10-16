using AutoMapper;
using CinemaSystem.Application.DTO.Auth;
using CinemaSystem.Core.Entities;

namespace CinemaSystem.Application.MappingProfiles
{
    internal class RoleMappingProfile : Profile
    {

        public RoleMappingProfile()
        {
            CreateMap<Role, RoleDto>();
        }
    }
}