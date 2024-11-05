using AutoMapper;
using CleanArchitecture.Application.Features.Auth.GetAll;
using CleanArchitecture.Application.Features.Auth.GetById;
using CleanArchitecture.Application.Features.Auth.Register;
using CleanArchitecture.Application.Features.Auth.Role.Update;
using CleanArchitecture.Domain.Entities.Auth;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, GetAllQueryResponse>();
            CreateMap<User, GetByIdQueryResponse>();
            CreateMap<IdentityUser, RegisterCommandResponse>();
            CreateMap<Role, GetAllQueryResponse>();
            CreateMap<Role, GetByIdQueryResponse>();
            CreateMap<Role, UpdateCommandResponse>();

        }
    }
}
