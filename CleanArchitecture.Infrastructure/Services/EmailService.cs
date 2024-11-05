using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Application.Features.Auth.ForgotPassword;

namespace CleanArchitecture.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            message.From = new MailAddress(_configuration["EmailSettings:From"]);
            message.To.Add(new MailAddress(toEmail));
            message.IsBodyHtml = bool.Parse(_configuration["EmailSettings:IsBodyHtml"]);
            message.Subject = subject;
            message.Body = body;
            smtp.Port = int.Parse(_configuration["EmailSettings:Port"]);
            smtp.Host = _configuration["EmailSettings:SmtpServer"];
            smtp.EnableSsl = bool.Parse(_configuration["EmailSettings:EnableSsl"]);
            smtp.UseDefaultCredentials = bool.Parse(_configuration["EmailSettings:UseDefaultCredentials"]);
            smtp.Credentials = new NetworkCredential(_configuration["EmailSettings:Username"], _configuration["EmailSettings:Password"]);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);
        }

        public async Task SendEmailConfirmationAsync(string toEmail, string token)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            message.From = new MailAddress(_configuration["EmailSettings:From"]);
            message.To.Add(new MailAddress(toEmail));
            message.IsBodyHtml = bool.Parse(_configuration["EmailSettings:IsBodyHtml"]);
            message.Subject = "Email Onayı";
            message.Body =
            $"<!DOCTYPE html>" +
            $"<html lang=\"tr\">" +
            $"<head>" +
            $"    <meta charset=\"UTF-8\">" +
            $"    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">" +
            $"    <title>E-posta Onayı</title>" +
            $"    <style>" +
            $"        body {{ " +
            $"            font-family: Arial, sans-serif;" +
            $"            background-color: #f4f4f4;" +
            $"            margin: 0;" +
            $"            padding: 20px;" +
            $"        }}" +
            $"        .container {{ " +
            $"            background-color: #ffffff;" +
            $"            padding: 20px;" +
            $"            border-radius: 5px;" +
            $"            box-shadow: 0 2px 5px rgba(0,0,0,0.1);" +
            $"            max-width: 600px;" +
            $"            margin: auto;" +
            $"        }}" +
            $"        .header {{ " +
            $"            background-color: #007bff;" +
            $"            color: white;" +
            $"            padding: 10px 0;" +
            $"            text-align: center;" +
            $"            border-radius: 5px 5px 0 0;" +
            $"        }}" +
            $"        .button {{ " +
            $"            background-color: #28a745;" +
            $"            color: white;" +
            $"            padding: 10px 15px;" +
            $"            text-decoration: none;" +
            $"            border-radius: 5px;" +
            $"            display: inline-block;" +
            $"            margin: 20px 0;" +
            $"        }}" +
            $"        .footer {{ " +
            $"            text-align: center;" +
            $"            font-size: 12px;" +
            $"            color: #777;" +
            $"            margin-top: 20px;" +
            $"        }}" +
            $"    </style>" +
            $"</head>" +
            $"<body>" +
            $"    <div class=\"container\">" +
            $"        <div class=\"header\">" +
            $"            <h1>E-posta Onayı</h1>" +
            $"        </div>" +
            $"        <p>Merhaba,</p>" +
            $"        <p>Hesabınızı onaylamak için lütfen aşağıdaki butona tıklayın:</p>" +
            $"        <a href='{_configuration["EmailSettings:EmailConfirmationLink"]}{token}' class=\"button\">Hesabı Onayla</a>" +
            $"        <p>Bu e-posta, bir hesap oluşturduğunuzda size gönderilmiştir. Eğer bu isteği siz yapmadıysanız, bu e-postayı görmezden gelebilirsiniz.</p>" +
            $"        <div class=\"footer\">" +
            $"            <p>© {DateTime.Now.Year} Şirket Adı. Tüm hakları saklıdır.</p>" +
            $"        </div>" +
            $"    </div>" +
            $"</body>" +
             $"</html>";
            smtp.Port = int.Parse(_configuration["EmailSettings:Port"]);
            smtp.Host = _configuration["EmailSettings:SmtpServer"];
            smtp.EnableSsl = bool.Parse(_configuration["EmailSettings:EnableSsl"]);
            smtp.UseDefaultCredentials = bool.Parse(_configuration["EmailSettings:UseDefaultCredentials"]);
            smtp.Credentials = new NetworkCredential(_configuration["EmailSettings:Username"], _configuration["EmailSettings:Password"]);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);
        }

        public async Task SendEmailForgotPasswordAsync(string toEmail, ForgotPasswordCommandResponse request)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            message.From = new MailAddress(_configuration["EmailSettings:From"]);
            message.To.Add(new MailAddress(toEmail));
            message.IsBodyHtml = bool.Parse(_configuration["EmailSettings:IsBodyHtml"]);
            message.Subject = "Yeni Şifreniz";
            message.Body =
            $"<!DOCTYPE html>" +
            $"<html lang=\"tr\">" +
            $"<head>" +
            $"<meta charset=\"UTF-8\">" +
            $"<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">" +
            $"<title>Şifremi Unuttum</title>" +
            $"<style>" +
            $"    body {{ font-family: Arial, sans-serif; margin: 0; padding: 0; }}" +
            $"    .container {{ width: 100%; max-width: 600px; margin: 0 auto; padding: 20px; }}" +
            $"    .header {{ background-color: #007bff; color: white; padding: 10px 0; text-align: center; }}" +
            $"    .content {{ margin: 20px 0; }}" +
            $"    .footer {{ text-align: center; font-size: 12px; color: #777; margin-top: 20px; }}" +
            $"</style>" +
            $"</head>" +
            $"<body>" +
            $"<div class=\"container\">" +
            $"    <div class=\"header\">" +
            $"        <h1>Şifremi Unuttum</h1>" +
            $"    </div>" +
            $"    <div class=\"content\">" +
            $"        <p>Merhaba,</p>" +
            $"        <p><b>{request.UserName}</b> kullanıcısına ait şifreniz: <b>{request.Password}</b></p>" +
            $"        <p></p>" +
            $"        <p>Eğer bu isteği siz yapmadıysanız, lütfen bu e-postayı dikkate almayın.</p>" +
            $"        <p>İyi günler dileriz.</p>" +
            $"    </div>" +
            $"    <div class=\"footer\">" +
            $"        <p>&copy; {DateTime.Now.Year} Şirket Adı. Tüm hakları saklıdır.</p>" +
            $"    </div>" +
            $"</div>" +
            $"</body>" +
            $"</html>";





            //message.Body = $"{request.UserName} kullanıcısına ait şifreniz: {request.Password}";
            smtp.Port = int.Parse(_configuration["EmailSettings:Port"]);
            smtp.Host = _configuration["EmailSettings:SmtpServer"];
            smtp.EnableSsl = bool.Parse(_configuration["EmailSettings:EnableSsl"]);
            smtp.UseDefaultCredentials = bool.Parse(_configuration["EmailSettings:UseDefaultCredentials"]);
            smtp.Credentials = new NetworkCredential(_configuration["EmailSettings:Username"], _configuration["EmailSettings:Password"]);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);
        }
    }
}
