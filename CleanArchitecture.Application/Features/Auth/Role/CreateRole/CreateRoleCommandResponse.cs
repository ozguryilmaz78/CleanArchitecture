namespace CleanArchitecture.Application.Features.Auth.Role.CreateRole
{
    public class CreateRoleCommandResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public string Description { get; set; }
        public string ConcurrencyStamp { get; set; }
    }
}
