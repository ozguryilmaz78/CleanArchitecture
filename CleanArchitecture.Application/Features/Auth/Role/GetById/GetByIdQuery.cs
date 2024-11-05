using CleanArchitecture.Application.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Auth.Role.GetById
{
    public class GetByIdQuery : IRequest<Result<GetByIdQueryResponse>>
    {
        public string Id { get; set; }
    }
}
