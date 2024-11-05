namespace CleanArchitecture.Application.Features.Auth.Role.GetAll
{
    public class GetAllQueryResponse
    {
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public string ConcurrencyStamp { get; set; } = string.Empty;
    }
}
