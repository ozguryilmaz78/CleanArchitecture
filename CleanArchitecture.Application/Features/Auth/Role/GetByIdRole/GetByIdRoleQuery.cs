using CleanArchitecture.Application.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Auth.Role.GetByIdRole
{
    public class GetByIdRoleQuery : IRequest<Result<GetByIdRoleQueryResponse>>
    {
        public string Id { get; set; }
    }
}
