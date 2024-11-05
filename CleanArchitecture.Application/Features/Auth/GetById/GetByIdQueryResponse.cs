namespace CleanArchitecture.Application.Features.Auth.GetById
{
    public class GetByIdQueryResponse
    {
        public string Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => string.Join(" ", FirstName, LastName);
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpires { get; set; }
        public string? EmailConfirmationCode { get; set; }
    }
}
