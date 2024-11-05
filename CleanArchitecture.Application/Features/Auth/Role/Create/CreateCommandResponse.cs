namespace CleanArchitecture.Application.Features.Auth.Role.Create
{
    public class CreateCommandResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public string Description { get; set; }
        public string ConcurrencyStamp { get; set; }
    }
}
