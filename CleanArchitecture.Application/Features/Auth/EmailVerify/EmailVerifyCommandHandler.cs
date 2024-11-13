using AutoMapper;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities.Auth;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace CleanArchitecture.Application.Features.Auth.EmailVerify
{
    public class EmailVerifyCommandHandler : IRequestHandler<EmailVerifyCommand, Result<EmailVerifyCommandResponse>>
    {
        private readonly IRepository<AppUser> _repository;

        public EmailVerifyCommandHandler(IRepository<AppUser> repository, IConfiguration configuration)
        {
            _repository = repository;
        }

        public async Task<Result<EmailVerifyCommandResponse>> Handle(EmailVerifyCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByExpressionWithTrackingAsync(p => p.EmailConfirmationCode == request.Token, cancellationToken);
            if (user == null) return Result<EmailVerifyCommandResponse>.Failure("Kayıt bulunamadı.");
            user.EmailConfirmed = true;
            _repository.Update(user);
            var response = new EmailVerifyCommandResponse
            {
                Message = "Kullanıcının maili onaylandı."
            };
            return Result<EmailVerifyCommandResponse>.Succeed(response);
        }
    }
}
