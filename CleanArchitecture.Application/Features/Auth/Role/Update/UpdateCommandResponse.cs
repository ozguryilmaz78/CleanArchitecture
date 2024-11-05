namespace CleanArchitecture.Application.Features.Auth.Role.Update
{
    public class UpdateCommandResponse
    {
        public string Name { get; set; } = string.Empty;
        public string NormalizedName { get; set; } = string.Empty;
        public string ConcurrencyStamp { get; set; } = string.Empty;
    }
}
