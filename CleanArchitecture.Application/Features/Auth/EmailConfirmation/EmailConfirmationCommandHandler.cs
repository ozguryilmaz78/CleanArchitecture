using CleanArchitecture.Application.Features.Auth.GetAll;
using CleanArchitecture.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Application.Features.Auth.EmailConfirmation
{
    public class EmailConfirmationCommandHandler : IRequestHandler<EmailConfirmationCommand, Result<EmailConfirmationCommandResponse>>
    {
        private readonly IEmailService _emailService;

        public EmailConfirmationCommandHandler(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task<Result<EmailConfirmationCommandResponse>> Handle(EmailConfirmationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var emailResponse = new EmailConfirmationCommandResponse(request.Token);
                await _emailService.SendEmailConfirmationAsync(request.ToEmail,request.Token);
                return Result<EmailConfirmationCommandResponse>.Succeed(emailResponse);
            }
            catch (Exception ex)
            {
                // Hatanın detayını loglayabilirsiniz, burada sadece genel bir mesaj döneceğiz
                return Result<EmailConfirmationCommandResponse>.Failure("Mail gönderilemedi. Hata: " + ex.Message);
            }
        }
    }
}
