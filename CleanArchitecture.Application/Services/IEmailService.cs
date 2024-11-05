using CleanArchitecture.Application.Features.Auth.ForgotPassword;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail,  string subject, string body);
        Task SendEmailConfirmationAsync(string toEmail, string token);
        Task SendEmailForgotPasswordAsync(string toEmail, ForgotPasswordCommandResponse request);
    }
}
