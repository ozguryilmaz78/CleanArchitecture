using AutoMapper;
using CleanArchitecture.Application.Features.Auth.GetAll;
using CleanArchitecture.Application.Features.Auth.GetById;
using CleanArchitecture.Application.Features.Auth.Register;
using CleanArchitecture.Application.Features.Auth.Role.AssignRole;
using CleanArchitecture.Application.Features.Auth.Role.GetAllRole;
using CleanArchitecture.Application.Features.Auth.Role.GetByIdRole;
using CleanArchitecture.Application.Features.Auth.Role.UpdateRole;
using CleanArchitecture.Domain.Entities.Auth;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AppUser, GetAllQueryResponse>();
            CreateMap<AppUser, GetByIdQueryResponse>();
            CreateMap<AppUser, AssignRoleCommandResponse>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src));
            CreateMap<IdentityUser, UpdateCommandResponse>();
            CreateMap<AppRole, GetAllRoleQueryResponse>();
            CreateMap<AppRole, GetByIdRoleQueryResponse>();
            CreateMap<AppRole, UpdateRoleCommandResponse>();

        }
    }
}
