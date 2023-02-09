namespace webapi.Infrastructure.Database.Entities
{
    public class Admin
    {
        public Guid AdminId { get; set; }
        public string AdminName { get; set; } = String.Empty;
        public string AdminPassword { get; set; } = String.Empty;
    }
}
