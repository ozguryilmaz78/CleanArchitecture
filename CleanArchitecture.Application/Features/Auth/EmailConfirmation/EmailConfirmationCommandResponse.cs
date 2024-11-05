namespace CleanArchitecture.Application.Features.Auth.EmailConfirmation
{
    public class EmailConfirmationCommandResponse
    {
        public EmailConfirmationCommandResponse(string token)
        {
            Token = token;
        }

        public string Token { get; set; }
    }
}
