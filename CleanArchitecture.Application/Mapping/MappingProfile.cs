using AutoMapper;
using CleanArchitecture.Application.Features.Auth.GetAll;
using CleanArchitecture.Application.Features.Auth.GetById;
using CleanArchitecture.Application.Features.Auth.Register;
using CleanArchitecture.Application.Features.Auth.Role.Assign;
using CleanArchitecture.Application.Features.Auth.Role.Update;
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
            CreateMap<AppUser, AssignCommandResponse>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src));
            CreateMap<IdentityUser, RegisterCommandResponse>();
            CreateMap<AppRole, GetAllQueryResponse>();
            CreateMap<AppRole, GetByIdQueryResponse>();
            CreateMap<AppRole, UpdateCommandResponse>();

        }
    }
}
