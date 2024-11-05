using CleanArchitecture.Application.Features.Auth.ChangePassword;
using CleanArchitecture.Application.Features.Auth.EmailConfirmation;
using CleanArchitecture.Application.Features.Auth.EmailVerify;
using CleanArchitecture.Application.Features.Auth.ForgotPassword;
using CleanArchitecture.Application.Features.Auth.GetAll;
using CleanArchitecture.Application.Features.Auth.GetById;
using CleanArchitecture.Application.Features.Auth.Login;
using CleanArchitecture.Application.Features.Auth.Register;
using CleanArchitecture.Application.Features.Auth.Role.Assign;
using CleanArchitecture.Application.Features.Auth.Role.Create;
using CleanArchitecture.Application.Features.Auth.Role.Delete;
using CleanArchitecture.Application.Features.Auth.Role.Update;
using CleanArchitecture.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebAPI.Controllers
{
    
    public class AuthController : ApiController
    {
        public AuthController(IMediator mediator) : base(mediator)
        {
        }
        
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> GetAll(GetAllQuery request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> GetById(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult>ChangePassword(ChangePasswordCommand request, CancellationToken cancellationToken)
        {

            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> SendEmailConfirmation(EmailConfirmationCommand request, CancellationToken cancellationToken)
        {

            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [AllowAnonymous]
        [HttpGet("{token}")]
        public async Task<IActionResult> EmailVerify([FromRoute] string token, CancellationToken cancellationToken)
        {
            var request = new EmailVerifyCommand
            {
                Token = token
            };
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [AllowAnonymous]
        [HttpGet("{emailOrUserName}")]
        public async Task<IActionResult> ForgotPassword([FromRoute] string emailOrUserName, CancellationToken cancellationToken)
        {
            var request = new ForgotPasswordCommand
            {
                EmailOrUserName = emailOrUserName
            };
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRole(UpdateCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(DeleteCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> GetAllRoles(GetAllQuery request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> GetByIdRole(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRole(AssignCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

    }
}
