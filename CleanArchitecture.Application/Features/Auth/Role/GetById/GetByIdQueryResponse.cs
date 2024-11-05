namespace CleanArchitecture.Application.Features.Auth.Role.GetById
{
    public class GetByIdQueryResponse
    {
        public string Name { get; set; } = string.Empty;
        public string NormalizedName { get; set; } = string.Empty;
        public string ConcurrencyStamp { get; set; } = string.Empty;
    }
}
