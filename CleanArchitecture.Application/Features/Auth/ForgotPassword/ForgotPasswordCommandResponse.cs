namespace CleanArchitecture.Application.Features.Auth.ForgotPassword
{
    public class ForgotPasswordCommandResponse
    {
        public string Password { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
