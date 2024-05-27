namespace Ticketing.Models
{
    public class OrganizationUser
    {
        public required string UserId { get; set; }
        public  ApplicationUser? User { get; set; }
        public required int OrganizationId { get; set; }
        public  Organization? Organization { get; set; }
    }
}

