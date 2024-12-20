﻿using CleanArchitecture.Application.Features.Auth.Register;
using CleanArchitecture.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleanArchitecture.Application.Features.Auth.Register
{
    public class RegisterCommand : IRequest<Result<UpdateCommandResponse>>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhotoUrl { get; set; } = string.Empty;
        [NotMapped]
        public IFormFile? PhotoFile{ get; set; }

    }
}
